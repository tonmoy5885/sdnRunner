
$(document).ready(function () {

    let runner = {};

    //////////////////////---------------------------------------------///////////////////////////

    const getSelectedData = () => {
        let type=ctype = null;;

        const sdnRunner = document.getElementsByName("sndRunner")
        if ( sdnRunner[0].checked == true ) {
            type = sdnRunner[0].value;
        }
        else if ( sdnRunner[1].checked == true ) {
            type = sdnRunner[1].value;
        }

        // Check Select all is checked
 
        var inputElements = document.getElementsByName("cloudType");
        for(var i=0; inputElements[i]; ++i){
            if(inputElements[i].checked){
                ctype = inputElements[i].value;
                break;
            }
        }

        return [ type , ctype ];
    }

    function getRunner(url, postdata){

        var reqeest = $.ajax({
            type: "get",
            url: 'assets/all.json',
            dataType: 'json',
            cache: false,
            async: false
        });
    
        return reqeest.responseJSON;
    
    }

    const appendToClass = (html) => {
        // Clear form 
        $("#form_class").empty();
        $("#form_class").append(html);

    }

    //####################### Main Scripts##############################
    runner = getRunner();

    const get_options = ( regions ) => {
        let regionSelect = ''
        for ( var i=0; i < regions.length ; i++ ) {
            regionSelect += '<option value="'+ regions[i] +'"> '+ regions[i] +' </option>';
        }
        return(regionSelect);
    }

    ///#################################################
    // Public
    $(document.body).on('change',"#EnvironmentName",function (e) {
        $("#AzureEnvironmentType").val( $(this).val() )
        $("#Region").empty();

        if ( $(this).val() == 'Prod' ) {
            let regionSelect = get_options(runner.public.Prod.Region)
            $("#Region").append(regionSelect)
        }

        else if ( $(this).val() == 'Canary' ) {
            let regionSelect = get_options(runner.public.Canary.Region)
            $("#Region").append(regionSelect)
        }

        else if ( $(this).val() == 'Pilot' ) {
            let regionSelect = get_options(runner.public.Pilot.Region)
            $("#Region").append(regionSelect)
        }
     });



    ///#################################################
    const all_append = (all) => {

        let configurations = '';

        for (var i in all ) {
            configurations += '<div class="form-group"> ' +
                                '<label class="control-label text-primary"> ' + i + ' </label>' + 
                                '<input type="text" class="form-control" name="' + i + '">' +
                              '</div>';
            $("#form_class").empty();
            $("#form_class").append(configurations);
            $("#form_class").append('<div class="text-center mb-4"><button class="btn btn-primary">Submit</button</div>');
        }


    }
    const Public_append = (Public) => {

        var environmentName =  '<label class="form-check-label text-primary" for="environment"> EnvironmentName </label>' +
                                '<select class="form-select" name="EnvironmentName" id="EnvironmentName">' +
                                    '<option value="Prod" selected >Prod</option>' +
                                    '<option value="Canary">Canary</option>' +
                                    '<option value="Pilot">Pilot</option>' +
                                '</select>' +
                                '<div class="form-group"> ' +
                                    '<label class="control-label text-primary"> CloudType </label>' + 
                                    '<input type="text" class="form-control" name="CloudType" value="Public">'  +
                                '</div>';

        var AzureEnvironmentType = '<div class="form-group"> ' +
                                        '<label class="control-label text-primary"> AzureEnvironmentType </label>' + 
                                        '<input type="text" class="form-control" name="AzureEnvironmentType" id="AzureEnvironmentType" value="Prod">' +
                                    '</div>';

        var regionSelect =  '<label class="form-check-label text-primary" for="Region"> Region </label>' +
                            '<select class="form-select" name="Region" id="Region">' +
                            get_options(Public.Prod.Region) +
                            '</select>';

        let configurations = '';

        for (var i in runner.all ) {
            configurations += '<div class="form-group"> ' +
                                '<label class="control-label text-primary"> ' + i + ' </label>' + 
                                '<input type="text" class="form-control" name="' + i + '">' +
                                '</div>';
        }

        
        $("#form_class").empty();
        
        $("#form_class").append(environmentName);
        $("#form_class").append(AzureEnvironmentType);
        $("#form_class").append(regionSelect);
        $("#form_class").append(configurations);

        $("#form_class").append('<div class="text-center mb-4"><button class="btn btn-primary">Submit</button</div>');


    }
    const Others_append = ( env  ) => {
        var environmentName =  '<div class="form-group"> ' +
                                    '<label class="control-label text-primary"> EnvironmentName </label>' + 
                                    '<input type="text" class="form-control" name="EnvironmentName" value="' + env.EnvironmentName +'">' +
                                '</div>'+
                                '<div class="form-group"> ' +
                                '<label class="control-label text-primary"> CloudType </label>' + 
                                '<input type="text" class="form-control" name="CloudType" value=' + env.Configurations.CloudType +'>'  +
                              '</div>';

        var AzureEnvironmentType = '<div class="form-group"> ' +
                                        '<label class="control-label text-primary"> AzureEnvironmentType </label>' + 
                                        '<input type="text" class="form-control" name="AzureEnvironmentType" value="'+ env.Configurations.AzureEnvironmentType +'">' +
                                    '</div>';
        let configurations = '';

        for (var i in env.Configurations ) {
            if ( i == 'AzureEnvironmentType' || i == 'Region') continue;
            configurations += '<div class="form-group"> ' +
                                '<label class="control-label text-primary"> ' + i + ' </label>' + 
                                '<input type="text" class="form-control" name="' + i + '">' +
                              '</div>';
        }

        var regionSelect =  '<label class="form-check-label text-primary" for="Region"> Region </label>' +
                            '<select class="form-select" name="Region" id="Region">' +
                                get_options(env.Region) +
                            '</select>';


        $("#form_class").empty();
        
        $("#form_class").append(environmentName);
        $("#form_class").append(AzureEnvironmentType);
        $("#form_class").append(regionSelect);
        $("#form_class").append(configurations);
        $("#form_class").append('<div class="text-center mb-4"><button class="btn btn-primary">Submit</button</div>');

        

    }
    /////////////////////////------------------- End Append ------------/
    /////////###########################################################
    $("#updateForm").click(function(){
        //Get 
        let selected = getSelectedData()

        if ( selected[1] == "all" ) {
            const All = runner.all;
            all_append(All);
        }
        else if ( selected[1] == "Public" ) {
            const Public = runner.public;
            Public_append(Public);
        }
        else if ( selected[1] == "Fairfax" ) {
            const Fairfax = runner.fairfax.FFProd;
            Others_append(Fairfax);
        }

        else if ( selected[1] == "Mooncake" ) {
            const Mooncake = runner.mooncake.MCProd;
            Others_append(Mooncake);
        }
        else if ( selected[1] == "USNat" ) {
            const USNat = runner.usnat.USNatProd;
            Others_append(USNat);
        }
        else if ( selected[1] == "USSec" ) {
            const USSec = runner.ussec.USSecProd;
            Others_append(USSec);
        }

        console.log(selected[0] , selected[1])
        // $("#form_class").append("<b>Appended text</b>");

      });

    console.log(runner)

});