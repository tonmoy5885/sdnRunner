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
    public class DevpModel : PageModel
    {
        public bool hasData = false;
        public bool isQuery = false;
        public string filepath = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            hasData = true;
        
            // Define A Dictionary
            Dictionary<string, string> collection = new Dictionary<string, string>();

            // Get Form Data In list (Collction.) 
            foreach (string key in Request.Form.Keys) {
                if ( key == "__RequestVerificationToken" ) {
                    break;
                }
                collection.Add( key  , Request.Form[key] );
            }

            // Generate Random Filename
            Random random = new Random();
            var randomNumber = random.Next(1000000, 9999999);
            string jsonFilename = randomNumber.ToString();
            filepath = "JsonFiles/" + jsonFilename + ".json";

            // Convert list to string in json format
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(collection , options );

            // Create file to "wwwroot/JsonFiles" folder
            System.IO.File.WriteAllText( "wwwroot/" +  filepath , jsonString);

        }

    }

}
   