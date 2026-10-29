[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$Path,

    [string]$OutputPath = $Path
)

$ErrorActionPreference = 'Stop'

$document = New-Object System.Xml.XmlDocument
$document.PreserveWhitespace = $false
$document.Load($ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($Path))

foreach ($testcase in $document.SelectNodes('//testcase')) {
    $testName = $testcase.GetAttribute('name')
    $separator = $testName.IndexOf('_')
    if ($separator -le 0) {
        throw "Testcase name '$testName' must start with an ID followed by '_'."
    }

    $properties = $testcase.SelectSingleNode('properties')
    if ($null -eq $properties) {
        $properties = $document.CreateElement('properties')
        [void]$testcase.AppendChild($properties)
    }

    $property = $properties.SelectSingleNode("property[@name='test_key']")
    if ($null -eq $property) {
        $property = $document.CreateElement('property')
        $property.SetAttribute('name', 'test_key')
        [void]$properties.AppendChild($property)
    }
    $property.SetAttribute('value', $testName.Substring(0, $separator))

    $testcase.RemoveAttribute('classname')
    $testcase.RemoveAttribute('name')
}

$settings = New-Object System.Xml.XmlWriterSettings
$settings.Indent = $true
$settings.IndentChars = '  '
$settings.NewLineChars = "`r`n"
$settings.NewLineHandling = [System.Xml.NewLineHandling]::Replace

$writer = [System.Xml.XmlWriter]::Create(
    $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($OutputPath),
    $settings
)
try {
    $document.Save($writer)
}
finally {
    $writer.Dispose()
}
