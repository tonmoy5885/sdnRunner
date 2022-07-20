using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FirstWebApp.Pages
{
    public class ConfigModel : PageModel
    {
        public bool hasData = false;
        public bool isQuery = false;

        public Dictionary<string, string> st = new Dictionary<string, string>
        {
            ["westus"] = "wus",
            ["eastus"] = "eus",
            ["eastus2"] = "eus2",
            ["eastus2euap"] = "eus2euap",
            ["northcentralus"] = "ncus",
            ["westus2"] = "wus2",
            ["southcentralus"] = "scus",
            ["centralus"] = "cus",
            ["centraluseuap"] = "cuseuap",
            ["westeurope"] = "weur",
            ["northeurope"] = "neur",
            ["eastasia"] = "easia",
            ["southeastasia"] = "seasia",
            ["japaneast"] = "jape",
            ["japanwest"] = "japw",
            ["brazilsouth"] = "bras",
            ["australiaeast"] = "ause",
            ["australiasoutheast"] = "ausse",
            ["southindia"] = "sind",
            ["westindia"] = "wind",
            ["centralindia"] = "cind",
            ["canadacentral"] = "canc",
            ["canadaeast"] = "cane",
            ["westcentralus"] = "wcus",
            ["ukwest"] = "ukw",
            ["uksouth"] = "uks",
            ["koreasouth"] = "kors",
            ["koreacentral"] = "korc",
            ["francecentral"] = "frc",
            ["francesouth"] = "frs",
            ["eastuseuap"] = "euseuap",
            ["eastus2(stage)"] = "eus2(stage)",
            ["uswestvalidation"] = "wusval",
            ["usgovvirginia"] = "usgovvir",
            ["usgoviowa"] = "usgoviowa",
            ["usdodcentral"] = "usdodc",
            ["usdodeast"] = "usdode",
            ["usgovtexas"] = "usgovtx",
            ["usgovarizona"] = "usgovaz",
            ["chinanorth"] = "chinan",
            ["chinaeast"] = "chinae",
            ["germanynortheast"] = "gerne",
            ["germanycentral"] = "gerc",
            ["chinaeast2"] = "chinae2",
            ["chinanorth2"] = "chinan2",
            ["australiacentral"] = "ausc",
            ["australiacentral2"] = "ausc2",
            ["southafricawest"] = "saw",
            ["southafricanorth"] = "san",
            ["uaecentral"] = "uaec",
            ["uaenorth"] = "uaen",
            ["switzerlandwest"] = "swizw",
            ["switzerlandnorth"] = "swizn",
            ["germanynorth"] = "gern",
            ["germanywestcentral"] = "gerwc",
            ["norwayeast"] = "nore",
            ["norwaywest"] = "norw",
            ["brazilsoutheast"] = "brase",
            ["westus3"] = "wus3",
            ["jioindiacentral"] = "jioindc",
            ["jioindiawest"] = "jioindw",
            ["swedensouth"] = "swes",
            ["swedencentral"] = "swec",
            ["chinaeast3"] = "chinae3",
            ["chinanorth3"] = "chinan3",
            ["qatarcentral"]=  "qatc"
        };

        public string filepath = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            hasData = true;


            List<string> RunnerControlPlaneValidations = new List<string>();
            List<string> InstanceCount = new List<string>();
            Dictionary<string, object> stages = new Dictionary<string, object>();
            List<string> substages = new List<string>();


            Console.WriteLine(Request.Form["UserMgdClientId"]);


            // Check How Many Instance
            foreach (string key in Request.Form.Keys) {
                if ( key.IndexOf("runnerFlaver") == 0 ){
                    string[] subs = key.Split('-');
                    InstanceCount.Add(subs[1]);
                    Console.WriteLine(subs[1]);
                }
                if ( key.IndexOf("rcpv") == 0 ){
                    RunnerControlPlaneValidations.Add(Request.Form[key]);
                }

                if ( key.IndexOf("runnerStage") == 0 ){
                    //stages.Add( key , Request.Form[key]);
                    string[] subs = key.Split('-');
                    string stage = subs[1];
                    substages.Clear();

                    foreach (string key2 in Request.Form.Keys ) {
                        if ( key2.IndexOf("runnerActivity-"+ stage + "-child-") == 0 ) {
                            substages.Add( Request.Form[key2] );
                        }
                        
                    }
                    stages.Add( Request.Form[key] ,  substages );
                }

            }

            Dictionary<string, object> main = new Dictionary<string, object>();

            var runnerFlaver = "";
            var region = "";
            var sdnRunner = "";
            var armTemplate = "";
            var subscriptionID = "";

            foreach( var i in InstanceCount ){

                Dictionary<string, object> instance = new Dictionary<string, object>();

                runnerFlaver = Request.Form["runnerFlaver-"+i];
                region = Request.Form["region-"+i];
                sdnRunner = Request.Form["sndRunner"];
                armTemplate = Request.Form["armTemplate-"+i];
                subscriptionID = Request.Form["subscriptionID-"+ i];
            
                
                instance.Add( "RunnerName" , Request.Form["RunnerName"]+"" );

                string runnerName = Request.Form["RunnerName"];
                string[] subs = runnerName.Split('.');
                runnerName = subs[1];
                // Console.WriteLine(runnerName);
                
                                                //  Runner Flavor + RunnerName + Region + Prod/Dev>
                instance.Add( "RunnerInstanceName" , runnerFlaver + runnerName + region + sdnRunner );
                instance.Add( "EnvironmentName" , runnerFlaver +  region + sdnRunner );
                instance.Add( "MdsServiceUrl" , Request.Form["MdsServiceUrl"]+"" );
                instance.Add( "MdsLocation" , Request.Form["MdsLocation"]+"" );
                instance.Add( "MdsAccountName" , Request.Form["MdsAccountName"]+"" );
                instance.Add( "MDSToolsRelativePath" , Request.Form["MDSToolsRelativePath"]+"" );
                instance.Add( "MdmAccountName" , Request.Form["MdmAccountName"]+"" );
                instance.Add( "MdmNamespace" , Request.Form["MdmNamespace"]+"" );
                instance.Add( "CertStoreLocation" , "CurrentUser" );
                instance.Add( "CIDR" , "" );
                instance.Add( "Zones" , "" );


                //
                instance.Add( "ClientCertThumbprint" , "dac3d81955b6b9a617fb7fb6508812a87231bfd3" );
                instance.Add( "DataPathMetricName" , "E2ERunnersDataPathMetric" );
                instance.Add( "DeploymentMetricName" , "LifeCycleMetric" );
                instance.Add( "TcpListenerSettingPath" , "ContentFiles/TcpListenerJson.txt" );
                instance.Add( "UdpListenerSettingPath" , "ContentFiles/UdpListenerJson.txt" );
                instance.Add( "TcpSendTimeoutSeconds" , "15" );
                instance.Add( "UdpSendTimeoutSeconds" , "15" );
                instance.Add( "MaxTcpSendAttempts" , "5" );
                instance.Add( "MaxUdpSendAttempts" , "5" );
                instance.Add( "TcpSendSettingPath" , "ContentFiles/TCPSenderJson.txt" );
                instance.Add( "UdpSendSettingPath" , "ContentFiles/UdpSenderJson.txt" );

                Dictionary<string, object> temp = new Dictionary<string, object>();
                temp.Add( "RawData" , null );
                instance.Add( "ClientCertificate" , temp );

                instance.Add( "ClientKey" , "" );
                instance.Add( "ExceptionIntervalInHours" , "1" );

                instance.Add( "CloudType" , null );
                
                //  Need tgo add mmore

                instance.Add( "ClientId" , Request.Form["ClientId"]+"" );
                instance.Add( "TenantId" , Request.Form["TenantId"]+"" );
                instance.Add( "AADServicePrincipalObjectId" , Request.Form["AADServicePrincipalObjectId"]+"" );

                instance.Add( "UserMgdClientId" , Request.Form["UserMgdClientId"]+"" );
                instance.Add( "UserMgdTenantId" , Request.Form["UserMgdTenantId"]+"" );
                instance.Add( "UserMgdObjectId" , Request.Form["UserMgdObjectId"]+"" );
                
                instance.Add( "DeploymentName" , "" ); //global

                instance.Add( "SubscriptionId" , subscriptionID + ""  );
                instance.Add( "Region" ,  region + "" );
                
                /////////////////////////////////////  " Flavor + RunnerName + Region Shortname + Prod/Dev>"
                instance.Add( "ResourceGroupName" , runnerFlaver + runnerName + st[region] + sdnRunner );
                instance.Add( "WorkerLogType" , "json");
                instance.Add( "TipClusters" , null);
                instance.Add( "CurrentRGStage" , 0 );

                instance.Add( "DataPathTestIntervalInSeconds" , 120 ); //global
                instance.Add( "TransformationIntervalInSeconds" , -1 );
                instance.Add( "CurrentTransformationDeploymentNumber" , 0 );
                instance.Add( "NumberOfTransformations" , 0 );
                instance.Add( "NumberOfResourceDeployments" , 0 );
                instance.Add( "RunnerRedeploymentIntervalInHours" , -1 );
                instance.Add( "ResourceGroupRedeploymentIntervalInHours" , 72 );
                instance.Add( "NumberOfRGsPerInstance" , 3 );

                List<string> tempList = new List<string>();
                instance.Add( "RunnerLifeCycle" , tempList );

                instance.Add( "ExceptionInterval" , 0 );
                
                instance.Add( "AuthorityText" , Request.Form["AuthorityText"]+"" );

                Dictionary<string, object> ARMTemplates = new Dictionary<string, object>(); 
                
                ARMTemplates.Add ("Identifier" ,  null );
                ARMTemplates.Add ("TemplatePath" , "ARMTemplates\\" + Request.Form["ARMTemplates"] + "\\" + armTemplate + ".json" );
                ARMTemplates.Add ("TemplateContents" , null );
                ARMTemplates.Add ("OutputVariables" , null );
                ARMTemplates.Add ("DeploymentDuration" , "00:00:00");
                ARMTemplates.Add ("IsPrimaryARMTemplate" , true );
                ARMTemplates.Add ("TransformationIntervalSeconds" , -1 );
                
                instance.Add( "ARMTemplates" , ARMTemplates);
                instance.Add( "SqlServerEndPointSuffix" , Request.Form["SqlServerEndPointSuffix"]+"" );
                instance.Add( "AzureManagementUrl" , Request.Form["AzureManagementUrl"]+"" );
                instance.Add( "KeyVaultResourceURL" , Request.Form["KeyVaultResourceURL"]+"" );


                instance.Add( "KeyVaultCertName" , "E2EWorkersCert" );
                instance.Add( "MACertificateName" , "SDNRunnerMA" );
                instance.Add( "KeyVaultNamePrefix" , "KVT" );
                instance.Add( "KeyVaultRGName" , "E2ERunnersKeyVault" );
                instance.Add( "KeyVaultConfigFileName" , Request.Form["KeyVaultConfigFileName"]+"" );
                instance.Add( "SubscriptionListFileName" , Request.Form["SubscriptionListFileName"]+"" );
                instance.Add( "EnableKeyVaultIntegration" , true );
                instance.Add( "IsBootstrapRunner" , false );
                instance.Add( "UseSubjectNameBasedAccessToAAD" , true );
                instance.Add( "IsLocalBox" , false );
                instance.Add( "DeleteRGAfterFailedDeploy" , true );
                instance.Add( "NonLegacySku" , false );
                instance.Add( "IsPrivateLink" , false );
                instance.Add( "RunnerNamespace" , "E2ETests" );
                instance.Add( "WorkerRoleUrl" , Request.Form["WorkerRoleUrl"]+"" );
                instance.Add( "PermissibleVmSku" , "" );
                instance.Add( "AzureEnvironmentType" , sdnRunner+"" );
                instance.Add( "StorageEndpointSuffix" , Request.Form["StorageEndpointSuffix"]+"" );
                instance.Add( "DeploymentMode" , "NoOp" );
                instance.Add( "CloudappFqdn" , Request.Form["CloudappFqdn"]+"" );
                instance.Add( "LMEnabled" , false );
                instance.Add( "SkipValidationTypes" , tempList );
                instance.Add( "SLBVersion" , "" );
                instance.Add( "SLBType" , "" );
                instance.Add( "ModifyPipSlbSku" , true );
                instance.Add( "RunnerFlavor" , runnerFlaver );
                instance.Add( "TestInstanceId" , "NotApplicable" );
                instance.Add( "Context" , null );
                instance.Add( "Environment" , runnerFlaver +  region + sdnRunner );
                instance.Add( "UserAssignedManagedIdentityConnectionString" , "" );
                instance.Add( "AdditionalMetricNames" , tempList );
                instance.Add( "GeoPairRegion" , "" );
                instance.Add( "ContainerGroupDataPathRegion" , "" );

                instance.Add( "ContainerGroupInfraRegion" , "" );
                instance.Add( "ValidationType" , Request.Form["validationType"]);

                instance.Add( "RunnerValidations" , stages);

                instance.Add( "AllowCertBasedAccessToRunMDSCommands" , false );
                instance.Add( "SkipArmTemplate" , false );
                instance.Add( "NetPerf" , false );
                instance.Add( "MdsNamespace" , "E2ETestsProd" );

                instance.Add( "RunnerControlPlaneValidations" , RunnerControlPlaneValidations);

                // FINALLY ADDED TO MAIN
                main.Add( runnerFlaver +  region + sdnRunner  , instance );
                
            }

            string jsonFilename = DateTime.Now.ToString("yyyy-MM-dd-h-mm-ss-tt");
            jsonFilename = jsonFilename.ToLower();
            filepath = "JsonFiles/" + jsonFilename + ".json";

            // Convert list to string in json format
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize( main , options );

            // Create file to "wwwroot/JsonFiles" folder
            System.IO.File.WriteAllText( "wwwroot/" +  filepath , jsonString);

        }

    }

}
   