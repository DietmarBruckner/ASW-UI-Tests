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
            self.exe_path = Path(__file__).resolve().parent / "bin" / "Debug" / "net481" / "FlaUILibrary.exe"
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
    def initialize_automation_studio(self, timeout=30, verbose=None):
        return self._call("initialize_automation_studio", timeout=self._to_seconds(timeout), verbose=verbose)

    @keyword("Close Application")
    def close_application(self, save_changes=True):
        return self._call("close_application", save_changes=self._to_bool(save_changes))

    @keyword("Invoke Menu")
    def invoke_menu(self, menu_name, menu_item=None, submenu_item=None):
        return self._call("invoke_menu", menu_name=menu_name, menu_item=menu_item, submenu_item=submenu_item)

    @keyword("Wait For Dialog")
    def wait_for_dialog(self, dialog_title, timeout=15):
        result = self._call("wait_for_dialog", dialog_title=dialog_title, timeout=self._to_seconds(timeout))
        return result == "found"

    @keyword("Wait For Message")
    def wait_for_message(self, message, timeout=30):
        return self._call("wait_for_message", message=message, timeout=self._to_seconds(timeout))

    @keyword("Type Into Dialog Field")
    def type_into_dialog_field(self, field_label, text):
        return self._call("type_into_field", field_label=field_label, text=text)

    @keyword("Type Slowly Into Dialog Field")
    def type_slowly_into_dialog_field(self, field_label, text):
        return self._call("type_slowly_into_field", field_label=field_label, text=text)

    @keyword("Set Dialog Field Value")
    def set_dialog_field_value(self, field_label, value):
        return self._call("set_field_value", field_label=field_label, value=value)

    @keyword("Click Dialog Button")
    def click_dialog_button(self, button_name="OK", dialog_title=None, dialog_close=False):
        return self._call("click_dialog_button", button_name=button_name, dialog_title=dialog_title, dialog_close=self._to_bool(dialog_close))

    @keyword("Click Toolbar Button")
    def click_toolbar_button(self, button_name, activate=False):
        return self._call("click_toolbar_button", button_name=button_name, activate=self._to_bool(activate))

    @keyword("Activate Tree Leaf")
    def activate_tree_leaf(self, viewtype, tree_path=None, editorname=None, rootname=None, program=False, shortcut=-1, single_click=False, filename=None, filetree=None, version=None):
        return self._call("activate_tree_leaf", viewtype=viewtype, tree_path=tree_path, editorname=editorname, rootname=rootname, program=self._to_bool(program), shortcut=shortcut, single_click=self._to_bool(single_click), filename=filename, filetree=filetree, version=version)

    @keyword("Select From ComboBox")
    def select_from_combo_box(self, combo_label, item_text):
        return self._call("select_from_combo_box", combo_label=combo_label, item_text=item_text)

    @keyword("Select From TreeComboBox")
    def select_from_tree_combo_box(self, item_label=None, item_number=-1):
        return self._call("select_from_tree_combo_box", item_label=item_label, item_number=item_number)

    @keyword("Wait For Idle")
    def wait_for_idle(self, timeout=30):
        return self._call("wait_for_idle", timeout=self._to_seconds(timeout))

    @keyword("Activate Simulation Mode")
    def activate_simulation_mode(self):
        return self._call("activate_simulation_mode")

    @keyword("Select Component Version")
    def select_component_version(self, component_name, version):
        return self._call("select_component_version", component_name=component_name, version=version)

    @keyword("Get Window Title")
    def get_window_title(self):
        return self._call("get_window_title")

    @keyword("Is Project Loaded")
    def is_project_loaded(self):
        return self._call("is_project_loaded")

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

    @keyword("Take Screenshot")
    def take_screenshot(self, filename=None, outputdir=None):
        return self._call("take_screenshot", filename=filename, outputdir=outputdir)

    @keyword("Insert From Toolbox")
    def insert_from_toolbox(self, view, component_name, category=None, drag=False, xoffset=0, yoffset=0):
        return self._call("insert_from_toolbox", view=view, category=category, component_name=component_name, drag=self._to_bool(drag), xoffset=xoffset, yoffset=yoffset)

    @keyword("Click into IDE")
    def click_into_ide(self, editor=False):
        return self._call("click_IDE", editor=self._to_bool(editor))

    @keyword("Add Role")
    def add_role(self, rolename, addrole=True):
        return self._call("add_role", rolename=rolename, add_role=self._to_bool(addrole))
    
    @keyword("Add User")
    def add_user(self, username, password, role, adduser=True):
        return self._call("add_user", username=username, password=password, role=role, add_user=self._to_bool(adduser))
    
    @keyword("Close Active Editor")
    def close_active_editor(self, save_changes=True):
        return self._call("close_active_editor", save_changes=self._to_bool(save_changes))
    
