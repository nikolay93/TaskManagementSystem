<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="TaskManagementService" generation="1" functional="0" release="0" Id="ca0c4137-bd80-4f86-8efe-479b9e03c2d1" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="TaskManagementServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="TaskManagementSystem:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/TaskManagementService/TaskManagementServiceGroup/LB:TaskManagementSystem:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="TaskManagementSystem:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/TaskManagementService/TaskManagementServiceGroup/MapTaskManagementSystem:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="TaskManagementSystem:TaskManagementEntitiesContainer" defaultValue="">
          <maps>
            <mapMoniker name="/TaskManagementService/TaskManagementServiceGroup/MapTaskManagementSystem:TaskManagementEntitiesContainer" />
          </maps>
        </aCS>
        <aCS name="TaskManagementSystemInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/TaskManagementService/TaskManagementServiceGroup/MapTaskManagementSystemInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:TaskManagementSystem:Endpoint1">
          <toPorts>
            <inPortMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystem/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapTaskManagementSystem:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystem/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapTaskManagementSystem:TaskManagementEntitiesContainer" kind="Identity">
          <setting>
            <aCSMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystem/TaskManagementEntitiesContainer" />
          </setting>
        </map>
        <map name="MapTaskManagementSystemInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystemInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="TaskManagementSystem" generation="1" functional="0" release="0" software="C:\Users\Nikolay\Documents\Visual Studio 2015\Projects\TaskManagementSystem\TaskManagementService\csx\Release\roles\TaskManagementSystem" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="TaskManagementEntitiesContainer" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;TaskManagementSystem&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;TaskManagementSystem&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystemInstances" />
            <sCSPolicyUpdateDomainMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystemUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystemFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="TaskManagementSystemUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="TaskManagementSystemFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="TaskManagementSystemInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="3fc06578-b920-4b27-a130-4da39ce11dcb" ref="Microsoft.RedDog.Contract\ServiceContract\TaskManagementServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="a703b258-8ced-497e-849c-3bd709b319a0" ref="Microsoft.RedDog.Contract\Interface\TaskManagementSystem:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/TaskManagementService/TaskManagementServiceGroup/TaskManagementSystem:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>