import json
import re
import subprocess
import time
from pathlib import Path

import requests
from robot.api.deco import keyword, library

@library(scope="GLOBAL")
class RobotFlaulib:
    """Robot Framework client for the FlaUILibrary HTTP keyword server."""

    def __init__(self, server_url="http://localhost:5000", exe_path=None, timeout=20):
        self.server_url = server_url.rstrip("/")
        self.timeout = int(timeout)
        self._session = requests.Session()
        self._session.headers.update({"Content-Type": "application/json"})
        self._server_process = None
        if exe_path:
            self.exe_path = Path(exe_path)
        else:
            self.exe_path = Path(__file__).resolve().parent / "bin" / "Release" / "net481" / "FlaUILibrary.exe"
        self._ensure_server()

    def _is_server_alive(self):
        try:
            resp = self._session.post(
                f"{self.server_url}/keyword/get_window_title",
                data="{}",
                timeout=2,
            )
            return resp.status_code == 200
        except requests.RequestException:
            return False

    def _ensure_server(self):
        if self._is_server_alive():
            return
        if not self.exe_path.exists():
            raise RuntimeError(
                f"FlaUILibrary executable not found at {self.exe_path}. Build FlaUILibrary.csproj first."
            )
        try:
            self._server_process = subprocess.Popen(
                [str(self.exe_path)],
                cwd=str(self.exe_path.parent.parent.parent.parent),
                stdout=subprocess.DEVNULL,
                stderr=subprocess.DEVNULL,
            )
        except OSError as exc:
            raise RuntimeError(f"Failed to start FlaUILibrary server: {exc}") from exc
        for _ in range(20):
            if self._is_server_alive():
                return
            time.sleep(0.25)
        raise RuntimeError("FlaUILibrary server did not become ready after auto-start")

    def _call(self, keyword_name, **kwargs):
        payload = {k: v for k, v in kwargs.items() if v is not None}
        url = f"{self.server_url}/keyword/{keyword_name}"
        def _invoke():
            response = self._session.post(
                url,
                data=json.dumps(payload),
                timeout=self.timeout,
            )
            response.raise_for_status()
            result = response.json()
            if "error" in result:
                raise RuntimeError(
                    f"FlaUILibrary keyword '{keyword_name}' failed: {result['error']}"
                )
            return result.get("result", result)
        try:
            return _invoke()
        except (requests.ConnectionError, requests.Timeout):
            self._ensure_server()
            return _invoke()

    @staticmethod
    def _to_bool(value):
        if isinstance(value, bool):
            return value
        if value is None:
            return False
        if isinstance(value, (int, float)):
            return bool(value)
        text = str(value).strip().lower()
        if text in {"true", "1", "yes", "y", "on"}:
            return True
        if text in {"false", "0", "no", "n", "off"}:
            return False
        raise ValueError(f"Invalid boolean value: {value}")

    @staticmethod
    def _to_seconds(value):
        if value is None:
            return None
        if isinstance(value, (int, float)):
            return int(value)
        text = str(value).strip().lower()
        if text.isdigit():
            return int(text)
        match = re.match(r"^([0-9]*\.?[0-9]+)\s*([smh])$", text)
        if not match:
            raise ValueError(f"Invalid timeout value: {value}")
        amount = float(match.group(1))
        unit = match.group(2)
        factor = 1 if unit == "s" else 60 if unit == "m" else 3600
        return int(amount * factor)

    @keyword("Initialize Automation Studio")
    def initialize_automation_studio(self, app_path, timeout=30):
        return self._call(
            "initialize_automation_studio",
            app_path=app_path,
            timeout=self._to_seconds(timeout),
        )

    @keyword("Close Application")
    def close_application(self, save_changes=True):
        return self._call("close_application", save_changes=self._to_bool(save_changes))

    @keyword("Invoke Menu")
    def invoke_menu(self, menu_name, menu_item=None, submenu_item=None):
        return self._call(
            "invoke_menu",
            menu_name=menu_name,
            menu_item=menu_item,
            submenu_item=submenu_item,
        )

    @keyword("Wait For Dialog")
    def wait_for_dialog(self, dialog_title, timeout=15):
        return self._call(
            "wait_for_dialog",
            dialog_title=dialog_title,
            timeout=self._to_seconds(timeout),
        )

    @keyword("Type Into Dialog Field")
    def type_into_dialog_field(self, field_label, text):
        return self._call("type_into_field", field_label=field_label, text=text)

    @keyword("Set Dialog Field Value")
    def set_dialog_field_value(self, field_label, value):
        return self._call("set_field_value", field_label=field_label, value=value)

"""     @keyword("Is Project Loaded")
    def is_project_loaded(self):
        return self._call("is_project_loaded")

    @keyword("Get Window Title")
    def get_window_title(self):
        return self._call("get_window_title")

    @keyword("Find UI Element")
    def find_ui_element(self, identifier, search_by="name", timeout=10):
        return self._call(
            "find_element",
            identifier=identifier,
            search_by=search_by,
            timeout=self._to_seconds(timeout),
        )

    @keyword("Wait For Element")
    def wait_for_element(self, identifier, search_by="name", timeout=10):
        return self._call(
            "wait_for_element",
            identifier=identifier,
            search_by=search_by,
            timeout=self._to_seconds(timeout),
        )

    @keyword("Element Should Exist")
    def element_should_exist(self, identifier, search_by="name"):
        result = self._call("element_exists", identifier=identifier, search_by=search_by)
        exists = result if isinstance(result, bool) else result.get("result", False)
        if not exists:
            raise AssertionError(f"Element not found: {identifier} (by {search_by})")
        return True

    @keyword("Click Element")
    def click_element(self, identifier, search_by="name"):
        return self._call("click_element", identifier=identifier, search_by=search_by)

    @keyword("Double Click Element")
    def double_click_element(self, identifier, search_by="name"):
        return self._call("double_click_element", identifier=identifier, search_by=search_by)

    @keyword("Right Click Element")
    def right_click_element(self, identifier, search_by="name"):
        return self._call("right_click_element", identifier=identifier, search_by=search_by)

    @keyword("Hover Element")
    def hover_element(self, identifier, search_by="name"):
        return self._call("hover_element", identifier=identifier, search_by=search_by)

    @keyword("Type Text")
    def type_text(self, text):
        return self._call("type_text", text=text)

    @keyword("Get Text From Element")
    def get_text_from_element(self, identifier, search_by="name"):
        result = self._call("get_text_from_element", identifier=identifier, search_by=search_by)
        return result if isinstance(result, str) else result.get("result", "")

    @keyword("Click Dialog Button")
    def click_dialog_button(self, button_name="OK", dialog_title=None):
        return self._call(
            "click_dialog_button",
            button_name=button_name,
            dialog_title=dialog_title,
        )

    @keyword("Get Dialog Field Text")
    def get_dialog_field_text(self, field_label, dialog_title=None):
        result = self._call(
            "get_dialog_text",
            field_label=field_label,
            dialog_title=dialog_title,
        )
        return result if isinstance(result, str) else result.get("result", "")

    @keyword("Open Context Menu")
    def open_context_menu(self, identifier, search_by="name"):
        return self._call("open_context_menu", identifier=identifier, search_by=search_by)

    @keyword("Select From Context Menu")
    def select_from_context_menu(self, menu_item, submenu_item=None):
        return self._call(
            "select_context_menu_item",
            menu_item=menu_item,
            submenu_item=submenu_item,
        )

    @keyword("Activate Tree Leaf")
    def activate_tree_leaf(self, tree_path, double_click=False):
        return self._call(
            "activate_tree_leaf",
            tree_path=tree_path,
            double_click=self._to_bool(double_click),
        )

    @keyword("Expand Tree Node")
    def expand_tree_node(self, node_name):
        return self._call("expand_tree_node", node_name=node_name)

    @keyword("Collapse Tree Node")
    def collapse_tree_node(self, node_name):
        return self._call("collapse_tree_node", node_name=node_name)

    @keyword("Set Property Value")
    def set_property_value(self, property_name, value):
        return self._call("set_property_value", property_name=property_name, value=value)

    @keyword("Get Property Value")
    def get_property_value(self, property_name):
        result = self._call("get_property_value", property_name=property_name)
        return result if isinstance(result, str) else result.get("result", "")

    @keyword("Wait For Build To Complete")
    def wait_for_build_to_complete(self, timeout=60):
        return self._call("wait_for_build", timeout=self._to_seconds(timeout))

    @keyword("Wait For Transfer To Complete")
    def wait_for_transfer_to_complete(self, timeout=120):
        return self._call("wait_for_transfer", timeout=self._to_seconds(timeout))

    @keyword("Wait For Idle")
    def wait_for_idle(self, timeout=30):
        return self._call("wait_for_idle", timeout=self._to_seconds(timeout))

    @keyword("Take Screenshot")
    def take_screenshot(self, filename=None, outputdir=None):
        return self._call("take_screenshot", filename=filename, outputdir=outputdir)
 """