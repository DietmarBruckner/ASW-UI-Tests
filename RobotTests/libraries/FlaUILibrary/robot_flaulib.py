import requests
import json
import re
from robot.api.deco import library, keyword

@library(scope='GLOBAL')
class RobotFlauiLib:
    """Python Robot Framework client for the FlaUILibrary HTTP keyword server.

    Usage in Robot Framework:
    Library    RobotTests/libraries/FlaUILibrary/robot_flaulib.py    server_url=http://localhost:5000
    """

    def __init__(self, server_url='http://localhost:5000'):
        self.server_url = server_url.rstrip('/')

    def _call(self, keyword_name, **kwargs):
        url = f"{self.server_url}/keyword/{keyword_name}"
        headers = {'Content-Type': 'application/json'}
        r = requests.post(url, data=json.dumps({k: v for k, v in kwargs.items() if v is not None}), headers=headers)
        r.raise_for_status()
        result = r.json()
        if 'error' in result:
            raise RuntimeError(f"FlaUILibrary keyword '{keyword_name}' failed: {result['error']}")
        return result.get('result', result)

    @staticmethod
    def _to_seconds(value):
        """Convert Robot timeout values (e.g. 10, 10s, 1m, 1.5h) to integer seconds."""
        if value is None:
            return None
        if isinstance(value, (int, float)):
            return int(value)

        text = str(value).strip().lower()
        if text.isdigit():
            return int(text)

        m = re.match(r"^([0-9]*\.?[0-9]+)\s*([smh])$", text)
        if not m:
            raise ValueError(f"Invalid timeout value: {value}")

        amount = float(m.group(1))
        unit = m.group(2)
        factor = 1 if unit == 's' else 60 if unit == 'm' else 3600
        return int(amount * factor)

    # ── IDE lifecycle ─────────────────────────────────────────────────────────

    @keyword('Initialize Automation Studio')
    def initialize_automation_studio(self, app_path, timeout=30):
        """Launch or attach to Automation Studio IDE."""
        return self._call('initialize_automation_studio', app_path=app_path, timeout=self._to_seconds(timeout))

    @keyword('Close Application')
    def close_application(self, save_changes=True):
        """Close the IDE application."""
        return self._call('close_application', save_changes=bool(save_changes))

    @keyword('Is Project Loaded')
    def is_project_loaded(self):
        """Return True if a project is currently loaded in the IDE."""
        return self._call('is_project_loaded')

    @keyword('Get Window Title')
    def get_window_title(self):
        """Return the title of the IDE main window."""
        return self._call('get_window_title')

    # ── Element finding ───────────────────────────────────────────────────────

    @keyword('Find UI Element')
    def find_ui_element(self, identifier, search_by='name', timeout=10):
        """Find a UI element by name, automationId, or controlType."""
        return self._call('find_element', identifier=identifier, search_by=search_by, timeout=self._to_seconds(timeout))

    @keyword('Wait For Element')
    def wait_for_element(self, identifier, search_by='name', timeout=10):
        """Wait for a UI element to appear."""
        return self._call('wait_for_element', identifier=identifier, search_by=search_by, timeout=self._to_seconds(timeout))

    @keyword('Element Should Exist')
    def element_should_exist(self, identifier, search_by='name'):
        """Assert that a UI element exists."""
        r = self._call('element_exists', identifier=identifier, search_by=search_by)
        exists = r if isinstance(r, bool) else r.get('result', False) if isinstance(r, dict) else False
        if not exists:
            raise AssertionError(f"Element not found: {identifier} (by {search_by})")
        return True

    # ── Element interaction ───────────────────────────────────────────────────

    @keyword('Click Element')
    def click_element(self, identifier, search_by='name'):
        """Click a UI element."""
        return self._call('click_element', identifier=identifier, search_by=search_by)

    @keyword('Double Click Element')
    def double_click_element(self, identifier, search_by='name'):
        """Double-click a UI element."""
        return self._call('double_click_element', identifier=identifier, search_by=search_by)

    @keyword('Right Click Element')
    def right_click_element(self, identifier, search_by='name'):
        """Right-click a UI element."""
        return self._call('right_click_element', identifier=identifier, search_by=search_by)

    @keyword('Hover Element')
    def hover_element(self, identifier, search_by='name'):
        """Hover the mouse over a UI element."""
        return self._call('hover_element', identifier=identifier, search_by=search_by)

    @keyword('Type Text')
    def type_text(self, text):
        """Type text using the keyboard."""
        return self._call('type_text', text=text)

    @keyword('Type Into Dialog Field')
    def type_into_dialog_field(self, field_label, text):
        """Type text into a labeled dialog field."""
        return self._call('type_into_field', field_label=field_label, text=text)

    @keyword('Set Dialog Field Value')
    def set_dialog_field_value(self, field_label, value):
        """Set value in a labeled dialog field."""
        return self._call('set_field_value', field_label=field_label, value=value)

    @keyword('Get Text From Element')
    def get_text_from_element(self, identifier, search_by='name'):
        """Read visible text from a UI element."""
        r = self._call('get_text_from_element', identifier=identifier, search_by=search_by)
        return r if isinstance(r, str) else r.get('result', '') if isinstance(r, dict) else str(r)

    # ── Dialog handling ───────────────────────────────────────────────────────

    @keyword('Click Dialog Button')
    def click_dialog_button(self, button_name='OK', dialog_title=None):
        """Click a button in a modal dialog."""
        return self._call('click_dialog_button', button_name=button_name, dialog_title=dialog_title)

    @keyword('Wait For Dialog')
    def wait_for_dialog(self, dialog_title, timeout=15):
        """Wait for a modal dialog with the given title to appear."""
        return self._call('wait_for_dialog', dialog_title=dialog_title, timeout=self._to_seconds(timeout))

    @keyword('Get Dialog Field Text')
    def get_dialog_field_text(self, field_label, dialog_title=None):
        """Get text from a field inside a dialog."""
        r = self._call('get_dialog_text', field_label=field_label, dialog_title=dialog_title)
        return r if isinstance(r, str) else r.get('result', '') if isinstance(r, dict) else str(r)

    # ── Menu interaction ──────────────────────────────────────────────────────

    @keyword('Invoke Menu')
    def invoke_menu(self, menu_name, menu_item=None, submenu_item=None):
        """Invoke a top-level menu, optionally clicking a menu item and submenu."""
        return self._call('invoke_menu', menu_name=menu_name, menu_item=menu_item, submenu_item=submenu_item)

    @keyword('Open Context Menu')
    def open_context_menu(self, identifier, search_by='name'):
        """Right-click an element to open its context menu."""
        return self._call('open_context_menu', identifier=identifier, search_by=search_by)

    @keyword('Select From Context Menu')
    def select_from_context_menu(self, menu_item, submenu_item=None):
        """Click an item in the currently visible context menu."""
        return self._call('select_context_menu_item', menu_item=menu_item, submenu_item=submenu_item)

    # ── Tree navigation ───────────────────────────────────────────────────────

    @keyword('Activate Tree Leaf')
    def activate_tree_leaf(self, tree_path, double_click=False):
        """Navigate and select a node in the project tree."""
        return self._call('activate_tree_leaf', tree_path=tree_path, double_click=bool(double_click))

    @keyword('Expand Tree Node')
    def expand_tree_node(self, node_name):
        """Expand a collapsed tree node."""
        return self._call('expand_tree_node', node_name=node_name)

    @keyword('Collapse Tree Node')
    def collapse_tree_node(self, node_name):
        """Collapse an expanded tree node."""
        return self._call('collapse_tree_node', node_name=node_name)

    # ── Property panel ────────────────────────────────────────────────────────

    @keyword('Set Property Value')
    def set_property_value(self, property_name, value):
        """Set a property in the IDE property panel."""
        return self._call('set_property_value', property_name=property_name, value=value)

    @keyword('Get Property Value')
    def get_property_value(self, property_name):
        """Get a property value from the IDE property panel."""
        r = self._call('get_property_value', property_name=property_name)
        return r if isinstance(r, str) else r.get('result', '') if isinstance(r, dict) else str(r)

    # ── Wait / synchronisation ────────────────────────────────────────────────

    @keyword('Wait For Build To Complete')
    def wait_for_build_to_complete(self, timeout=60):
        """Wait for the IDE build process to finish."""
        return self._call('wait_for_build', timeout=self._to_seconds(timeout))

    @keyword('Wait For Transfer To Complete')
    def wait_for_transfer_to_complete(self, timeout=120):
        """Wait for the IDE project transfer to finish."""
        return self._call('wait_for_transfer', timeout=self._to_seconds(timeout))

    @keyword('Wait For Idle')
    def wait_for_idle(self, timeout=30):
        """Wait for the IDE to become idle."""
        return self._call('wait_for_idle', timeout=self._to_seconds(timeout))

    # ── Screenshot ────────────────────────────────────────────────────────────

    @keyword('Take Screenshot')
    def take_screenshot(self, filename=None, outputdir=None):
        """Capture a screenshot of the IDE window."""
        return self._call('take_screenshot', filename=filename, outputdir=outputdir)
