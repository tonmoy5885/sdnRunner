
$(document).ready(function () {


    //########==== Necessary Functions =====############
    const get_options = ( regions ) => {
        let regionSelect = '';
        for ( var i=0; i < regions.length ; i++ ) {
            regionSelect += '<option value="'+ regions[i] +'"> '+ regions[i] +' </option>';
        }
        return regionSelect;
    }


    const settingsAppend = ( settings ) => {

        console.log("settings append is working ...... ")
        // Element name is id="dynamic-1"
        $("#dynamic-1").empty();
        let html = '';
        for ( let i in settings ) {
            html += '<div class="form-group">' +
                        '<label class="control-label" for="' + i + '"> <b>' + i + ' </b></label>' +
                        '<input type="text" class="form-control" id="' + i + '" name="' + i + '" value="' + settings[i] + '">' +
                    '</div>';
        }

        $("#dynamic-1").append( html );
    }
    

    ///#####################
    const getRunner = () => {

        var reqeest = $.ajax({
            type: "get",
            url: 'assets/all.json',
            dataType: 'json',
            cache: false,
            async: false
        });
        return reqeest.responseJSON;
    
    }
    
    let runnerData = getRunner();

    //######## Listener Functions ########//

    $(document).on('change', '.sndRunner', function() {

        if(this.checked) {
            if ( this.value == "prod") {
                settingsAppend(runnerData.prod);
            }else if ( this.value == "dev" ) {
                settingsAppend(runnerData.dev);
            }
        }
    });

    $(document).on('change', '.cloudType', function() {

        if(this.checked) {

            if ( this.value == "Public") {
                publicAppend(runnerData.public)
            }
            else if ( this.value == "Fairfax" ) {
                cloudAppend(runnerData.fairfax.FFProd)
            }
            else if ( this.value == "Mooncake" ) {
                cloudAppend(runnerData.mooncake.MCProd)
            }
            else if ( this.value == "USNat" ) {
                cloudAppend(runnerData.usnat.USNatProd)
            }
            else if ( this.value == "USSec" ) {
                cloudAppend(runnerData.ussec.USSecProd)
            }

        }
    });

    //----------------------------------------------------------


    //########==== Main Scripts =====############

    $(document.body).on('change',"#environment",function (e) {
        $("#Region").empty();

        if ( $(this).val() == 'Prod' ) {
            let regionSelect = get_options(runnerData.public.Prod.Region)
            $("#Region").append(regionSelect)
        }

        else if ( $(this).val() == 'Canary' ) {
            let regionSelect = get_options(runnerData.public.Canary.Region)
            $("#Region").append(regionSelect)
        }

        else if ( $(this).val() == 'Pilot' ) {
            let regionSelect = get_options(runnerData.public.Pilot.Region)
            $("#Region").append(regionSelect)
        }
     });

     const cloudAppend = ( env  ) => {
        
        var environmentName =  '<div class="form-group"> ' +
                                    '<label class="control-label text-primary"> <b>CloudType </b></label>' + 
                                    '<input type="text" class="form-control" name="CloudType" value=' + env.Configurations.CloudType +'>'  +
                                '</div>';

        var AzureEnvironmentType = '<div class="form-group"> ' +
                                        '<label class="control-label text-primary"><b> AzureEnvironmentType </b></label>' + 
                                        '<input type="text" class="form-control" name="AzureEnvironmentType" value="'+ env.Configurations.AzureEnvironmentType +'">' +
                                    '</div>';
        

        var regionSelect =  '<label class="form-check-label text-primary" for="Region"><b> Region </b></label>' +
                            '<select class="form-select" name="Region" id="Region">' +
                                get_options(env.Region) +
                            '</select>';

        let configurations = '';

        for (var i in env.Configurations ) {
            if ( i == 'MdsLocation' || i == 'CloudType' || i == 'AzureEnvironmentType' || i == 'Region') continue;
            configurations += '<div class="form-group"> ' +
                                '<label class="control-label text-primary"><b> ' + i + ' </b></label>' + 
                                '<input type="text" class="form-control" name="' + i + '" value="' + env.Configurations[i] + '">' +
                                '</div>';
        }


        $("#dynamic-2").empty();
        
        $("#dynamic-2").append(environmentName);
        $("#dynamic-2").append(AzureEnvironmentType);
        $("#dynamic-2").append(regionSelect);
        $("#dynamic-2").append(configurations);
        $("#dynamic-2").append('<div class="text-center mb-4"><button class="btn btn-primary">Submit</button</div>');

        

    }

    const publicAppend = (Public) => {

        var environmentName =  '<div class="form-group"> ' +
                                    '<label class="control-label text-primary"> <b>CloudType</b> </label>' + 
                                    '<input type="text" class="form-control" name="CloudType" value="Public">'  +
                                '</div>'+

                                '<label class="form-check-label text-primary" for="environment"> <b>AzureEnvironmentType</b> </label>' +
                                '<select class="form-select" name="AzureEnvironmentType" id="environment">' +
                                    '<option value="Prod" selected >Prod</option>' +
                                    '<option value="Canary">Canary</option>' +
                                    '<option value="Pilot">Pilot</option>' +
                                '</select>' ;
                                
        var regionSelect =  '<label class="form-check-label text-primary" for="Region"> <b>Region</b> </label>' +
                            '<select class="form-select" name="Region" id="Region">' +
                            get_options(Public.Prod.Region) +
                            '</select>';

        let configurations = '';

        let Fairfax = runnerData.fairfax.FFProd.Configurations;
        for (var i in Fairfax ) {
            if ( i == 'CloudType' || i=='MdsLocation' || i=='AzureEnvironmentType'|| i=='Region') continue;
            configurations += '<div class="form-group"> ' +
                                '<label class="control-label text-primary"> <b>' + i + '</b></label>' + 
                                '<input type="text" class="form-control" name="' + i + '" value="' + runnerData.all[i] + '">' +
                                '</div>';
        }

        
        $("#dynamic-2").empty();
        
        $("#dynamic-2").append(environmentName);
        $("#dynamic-2").append(regionSelect);
        $("#dynamic-2").append(configurations);

        $("#dynamic-2").append('<div class="text-center mb-4"><button class="btn btn-primary">Submit</button</div>');


    }

    publicAppend(runnerData.public);
    settingsAppend(runnerData.prod);
    

});