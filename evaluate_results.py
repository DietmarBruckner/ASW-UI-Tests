"""
Summarize Robot Framework results across many result folders.

Each top-level folder (1, 2, 3, ... 150) contains subfolders (ar, as, close_all,
mappview, opcua) that each hold an output.xml/log.html/report.html from a
Robot Framework run. This script walks all output.xml files, aggregates
pass/fail counts per run-folder and per suite, and lists failed tests.

Usage:
    python evaluate_results.py [root_dir]

Requires the "robotframework" package (pip install robotframework).
"""
import sys
from pathlib import Path
from collections import defaultdict

from robot.api import ExecutionResult
from robot.result.visitor import ResultVisitor


class Collector(ResultVisitor):
    def __init__(self):
        self.total = 0
        self.passed = 0
        self.failed = 0
        self.failures = []

    def visit_test(self, test):
        self.total += 1
        if test.passed:
            self.passed += 1
        else:
            self.failed += 1
            self.failures.append((test.longname, test.message))


def main():
    root = Path(sys.argv[1]) if len(sys.argv) > 1 else Path(__file__).parent
    output_files = sorted(root.glob("*/*/output.xml"), key=lambda p: (int(p.parts[-3]) if p.parts[-3].isdigit() else p.parts[-3], p.parts[-2]))

    if not output_files:
        print(f"No output.xml files found under {root}")
        return

    grand_total = grand_passed = grand_failed = 0
    per_suite_type = defaultdict(lambda: [0, 0])  # name -> [passed, failed]
    all_failures = []

    for output_file in output_files:
        run_id = output_file.parts[-3]
        suite_type = output_file.parts[-2]

        result = ExecutionResult(str(output_file))
        collector = Collector()
        result.visit(collector)

        grand_total += collector.total
        grand_passed += collector.passed
        grand_failed += collector.failed
        per_suite_type[suite_type][0] += collector.passed
        per_suite_type[suite_type][1] += collector.failed

        for name, message in collector.failures:
            all_failures.append((run_id, suite_type, name, message))

    print("=" * 70)
    print(f"Scanned {len(output_files)} output.xml files under {root}")
    print("=" * 70)

    print("\nResults by suite type:")
    for suite_type, (passed, failed) in sorted(per_suite_type.items()):
        total = passed + failed
        rate = (passed / total * 100) if total else 0
        print(f"  {suite_type:12} passed={passed:4} failed={failed:4} total={total:4} ({rate:5.1f}% pass)")

    print("\nOverall:")
    rate = (grand_passed / grand_total * 100) if grand_total else 0
    print(f"  passed={grand_passed} failed={grand_failed} total={grand_total} ({rate:.1f}% pass)")

    if all_failures:
        print(f"\nFailed tests ({len(all_failures)}):")
        for run_id, suite_type, name, message in all_failures:
            first_line = message.splitlines()[0] if message else ""
            print(f"  [{run_id}/{suite_type}] {name}: {first_line}")
    else:
        print("\nNo failed tests.")


if __name__ == "__main__":
    main()
