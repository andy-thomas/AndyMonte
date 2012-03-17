<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AndyMonte.Azure" generation="1" functional="0" release="0" Id="f96bf440-0a60-400f-a5dc-77e98086d52d" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AndyMonte.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AndyMonte:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/LB:AndyMonte:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AndyMonte:AndyMonteStorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapAndyMonte:AndyMonteStorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="AndyMonte:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapAndyMonte:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AndyMonteInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapAndyMonteInstances" />
          </maps>
        </aCS>
        <aCS name="ProjectCalculatorWorkerRole:AndyMonteStorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapProjectCalculatorWorkerRole:AndyMonteStorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="ProjectCalculatorWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapProjectCalculatorWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ProjectCalculatorWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapProjectCalculatorWorkerRoleInstances" />
          </maps>
        </aCS>
        <aCS name="SimulationRunWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapSimulationRunWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="SimulationRunWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/MapSimulationRunWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AndyMonte:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/AndyMonte/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapAndyMonte:AndyMonteStorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/AndyMonte/AndyMonteStorageConnectionString" />
          </setting>
        </map>
        <map name="MapAndyMonte:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/AndyMonte/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAndyMonteInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/AndyMonteInstances" />
          </setting>
        </map>
        <map name="MapProjectCalculatorWorkerRole:AndyMonteStorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/ProjectCalculatorWorkerRole/AndyMonteStorageConnectionString" />
          </setting>
        </map>
        <map name="MapProjectCalculatorWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/ProjectCalculatorWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapProjectCalculatorWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/ProjectCalculatorWorkerRoleInstances" />
          </setting>
        </map>
        <map name="MapSimulationRunWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/SimulationRunWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapSimulationRunWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/SimulationRunWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AndyMonte" generation="1" functional="0" release="0" software="C:\Projects\Personal\AndyMonte\Trial1\AndyMonte\AndyMonte.Azure\csx\Debug\roles\AndyMonte" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="768" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="AndyMonteStorageConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AndyMonte&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AndyMonte&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ProjectCalculatorWorkerRole&quot; /&gt;&lt;r name=&quot;SimulationRunWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/AndyMonteInstances" />
            <sCSPolicyFaultDomainMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/AndyMonteFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="ProjectCalculatorWorkerRole" generation="1" functional="0" release="0" software="C:\Projects\Personal\AndyMonte\Trial1\AndyMonte\AndyMonte.Azure\csx\Debug\roles\ProjectCalculatorWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="768" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="AndyMonteStorageConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ProjectCalculatorWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AndyMonte&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ProjectCalculatorWorkerRole&quot; /&gt;&lt;r name=&quot;SimulationRunWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/ProjectCalculatorWorkerRoleInstances" />
            <sCSPolicyFaultDomainMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/ProjectCalculatorWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="SimulationRunWorkerRole" generation="1" functional="0" release="0" software="C:\Projects\Personal\AndyMonte\Trial1\AndyMonte\AndyMonte.Azure\csx\Debug\roles\SimulationRunWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;SimulationRunWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AndyMonte&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ProjectCalculatorWorkerRole&quot; /&gt;&lt;r name=&quot;SimulationRunWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/SimulationRunWorkerRoleInstances" />
            <sCSPolicyFaultDomainMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/SimulationRunWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="AndyMonteFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="ProjectCalculatorWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="SimulationRunWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AndyMonteInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="ProjectCalculatorWorkerRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="SimulationRunWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="d6b2a2bb-f3b5-4de8-838f-293cbc6b9646" ref="Microsoft.RedDog.Contract\ServiceContract\AndyMonte.AzureContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="50c3c475-b522-4ece-94f0-c600d6863a16" ref="Microsoft.RedDog.Contract\Interface\AndyMonte:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/AndyMonte.Azure/AndyMonte.AzureGroup/AndyMonte:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>