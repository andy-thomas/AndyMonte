<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AndyMonte" generation="1" functional="0" release="0" Id="de7e1b25-c88f-4e1f-ae2d-7019f252da1a" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AndyMonteGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AndyMonte.Web:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AndyMonte/AndyMonteGroup/LB:AndyMonte.Web:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AndyMonte.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapAndyMonte.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AndyMonte.WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapAndyMonte.WebInstances" />
          </maps>
        </aCS>
        <aCS name="ProjectCalculatorWorkerRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapProjectCalculatorWorkerRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ProjectCalculatorWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapProjectCalculatorWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ProjectCalculatorWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapProjectCalculatorWorkerRoleInstances" />
          </maps>
        </aCS>
        <aCS name="SimulationRunWorkerRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapSimulationRunWorkerRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="SimulationRunWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapSimulationRunWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="SimulationRunWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AndyMonte/AndyMonteGroup/MapSimulationRunWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AndyMonte.Web:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AndyMonte/AndyMonteGroup/AndyMonte.Web/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapAndyMonte.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte/AndyMonteGroup/AndyMonte.Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAndyMonte.WebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AndyMonte/AndyMonteGroup/AndyMonte.WebInstances" />
          </setting>
        </map>
        <map name="MapProjectCalculatorWorkerRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte/AndyMonteGroup/ProjectCalculatorWorkerRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapProjectCalculatorWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte/AndyMonteGroup/ProjectCalculatorWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapProjectCalculatorWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AndyMonte/AndyMonteGroup/ProjectCalculatorWorkerRoleInstances" />
          </setting>
        </map>
        <map name="MapSimulationRunWorkerRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte/AndyMonteGroup/SimulationRunWorkerRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapSimulationRunWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AndyMonte/AndyMonteGroup/SimulationRunWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapSimulationRunWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AndyMonte/AndyMonteGroup/SimulationRunWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AndyMonte.Web" generation="1" functional="0" release="0" software="C:\Projects\AndyMonte\Trial2\src\AndyMonte\AndyMonte\csx\Debug\roles\AndyMonte.Web" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AndyMonte.Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AndyMonte.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ProjectCalculatorWorkerRole&quot; /&gt;&lt;r name=&quot;SimulationRunWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AndyMonte/AndyMonteGroup/AndyMonte.WebInstances" />
            <sCSPolicyFaultDomainMoniker name="/AndyMonte/AndyMonteGroup/AndyMonte.WebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="ProjectCalculatorWorkerRole" generation="1" functional="0" release="0" software="C:\Projects\AndyMonte\Trial2\src\AndyMonte\AndyMonte\csx\Debug\roles\ProjectCalculatorWorkerRole" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ProjectCalculatorWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AndyMonte.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ProjectCalculatorWorkerRole&quot; /&gt;&lt;r name=&quot;SimulationRunWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AndyMonte/AndyMonteGroup/ProjectCalculatorWorkerRoleInstances" />
            <sCSPolicyFaultDomainMoniker name="/AndyMonte/AndyMonteGroup/ProjectCalculatorWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="SimulationRunWorkerRole" generation="1" functional="0" release="0" software="C:\Projects\AndyMonte\Trial2\src\AndyMonte\AndyMonte\csx\Debug\roles\SimulationRunWorkerRole" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;SimulationRunWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AndyMonte.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ProjectCalculatorWorkerRole&quot; /&gt;&lt;r name=&quot;SimulationRunWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AndyMonte/AndyMonteGroup/SimulationRunWorkerRoleInstances" />
            <sCSPolicyFaultDomainMoniker name="/AndyMonte/AndyMonteGroup/SimulationRunWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="AndyMonte.WebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="ProjectCalculatorWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="SimulationRunWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AndyMonte.WebInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="ProjectCalculatorWorkerRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="SimulationRunWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="048baaa7-e326-4a8a-851b-fcd6916d98bc" ref="Microsoft.RedDog.Contract\ServiceContract\AndyMonteContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="b68a819c-63da-41be-97ba-82c45f3f67a9" ref="Microsoft.RedDog.Contract\Interface\AndyMonte.Web:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/AndyMonte/AndyMonteGroup/AndyMonte.Web:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>