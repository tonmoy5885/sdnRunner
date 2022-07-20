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
    public class GeneratorModel : PageModel
    {
        public bool hasData = false;
        public string filepath = "";

        public List<string> fields = new List<string>() {

            "RunnerName",
            "RunnerDescription",
            "RunnerInstanceName",
            "EnvironmentName",
            "MdsServiceUrl",
            "MdsLocation",
            "MdsAccountName",
            "MDSToolsRelativePath",
            "MdmAccountName",
            "MdmNamespace",
            "CertStoreLocation",
            "CIDR",
            "Zones",
            "DataPathMetricName",
            "DeploymentMetricName",
            "TcpListenerSettingPath",
            "UdpListenerSettingPath",
            "TcpSendTimeoutSeconds",
            "UdpSendTimeoutSeconds",
            "MaxTcpSendAttempts",
            "MaxUdpSendAttempts",
            "TcpSendSettingPath",
            "UdpSendSettingPath",
            "ClientCertificate",
            "ClientKey",
            "ExceptionIntervalInHours",
            "CloudType",
            "ClientId",
            "TenantId",
            "AADServicePrincipalObjectId",
            "UseManagedIdentity",
            "UserMgdClientId",
            "UserMgdTenantId",
            "UserMgdObjectId",
            "DeploymentName",
            "SubscriptionId",
            "Region",
            "WorkerLogType",
            "TipClusters",
            "CurrentRGStage",
            "DataPathTestIntervalInSeconds",
            "TransformationIntervalInSeconds",
            "NumberOfTransformations",
            "NumberOfResourceDeployments",
            "RunnerRedeploymentIntervalInHours",
            "ResourceGroupRedeploymentIntervalInHours",
            "NumberOfRGsPerInstance",
            "RunnerLifeCycle",
            "ExceptionInterval",
            "AuthorityText",
            "Identifier",
            "templatePath",
            "TemplateContents",
            "OutputVariables",
            "DeploymentDuration",
            "IsPrimaryARMTemplate",
            "TransformationIntervalSeconds",
            "SqlServerEndpointSuffix",
            "AzureManagementUrl",
            "KeyVaultResourceURL",
            "KeyVaultCertName",
            "MACertificateName",
            "KeyVaultNamePrefix",
            "KeyVaultRGName",
            "KeyVaultConfigFileName",
            "SubscriptionListFileName",
            "EnableKeyVaultIntegration",
            "IsBootstrapRunner",
            "UseSubjectNameBasedAccessToAAD",
            "IsLocalBox",
            "DeleteRGAfterFailedDeploy",
            "NonLegacySku",
            "IsPrivateLink",
            "RunnerNamespace",
            "WorkerRoleUrl",
            "PermissibleVmSku",
            "AzureEnvironmentType",
            "StorageEndpointSuffix",
            "DeploymentMode",
            "CloudappFqdn",
            "LMEnabled",
            "SkipValidationTypes",
            "SLBVersion",
            "SLBType",
            "ModifyPipSlbSku",
            "RunnerFlavor",
            "TestInstanceId",
            "Context",
            "Environment",
            "userAssignedManagedIdentityConnectionString",
            "AdditionalMetricNames",
            "GeoPairRegion",
            "ContainerGroupDataPathRegion",
            "ContainerGroupInfraRegion",
            "ValidationType",
            "RunnerValidations",
            "AllowCertBasedAccessToRunMDSCommands",
            "SkipArmTemplate",
            "NetPerf",
            "MdsNamespace"
         };

        public void OnGet()
        {
        }

        public void OnPost()
        {
            hasData = true;

            // Define A Dictionary
            Dictionary<string, string> collection = new Dictionary<string, string>();

            // Get Form Data In list (Collction) 
            foreach (string key in Request.Form.Keys)
            {
                if (key == "__RequestVerificationToken")
                {
                    break;
                }
                collection.Add(key, Request.Form[key]);
            }

            // Generate Random Filename
            Random random = new Random();
            var randomNumber = random.Next(1000000, 9999999);
            string jsonFilename = randomNumber.ToString();
            filepath = "JsonFiles/" + jsonFilename + ".json";

            // Convert list to string in json format
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(collection, options);

            // Create file to "wwwroot/JsonFiles" folder
            System.IO.File.WriteAllText("wwwroot/" + filepath, jsonString);

        }

    }
}
