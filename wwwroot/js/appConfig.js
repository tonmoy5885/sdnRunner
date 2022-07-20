
$(document).ready(function () {

    const getARMfiles = ( ARMTemplates ) => {
        arm = ''
        for ( i in ARMTemplates ) {
            template = ARMTemplates[i].replace(".json", "");
            arm += '<option value="'+ template +'"> '+ template +' </option>';
        }
        return arm;
    }

    serial = 0;

    let regions = localStorage.getItem('regions');
    let ARMTemplates = JSON.parse(localStorage.getItem('ARMTemplates'));
    let templateFile = getARMfiles(ARMTemplates.Accelnet)

    const getRunnerConfig = ( region , arm , serial) => {
        html = '<div class="rc-container row" id="instance-'+ serial +'">' +
                    '<div class="col-1"><i class="fa-solid fa-plus mt-2 rConfig-plus"></i></div>' +
                    '<div class="col-10 bg-light round">' +
                        '<div class="row">' +
                            '<div class="col-sm-6 form-group mb-2">' +
                                '<label class="control-label"> <b> Runner Flavor </b></label>' +
                                '<input type="text" class="form-control" name="runnerFlaver-'+ serial +'">' +
                            '</div>' +

                            '<div class="col-sm-6 form-group mb-2">' +
                                '<label class="control-label" for="region"> <b> Region </b></label>' +
                                '<select class="form-control instanceRegion" name="region-'+ serial +'">' + region +
                                '</select>' +
                            '</div>' +
                        '</div>' +

                        '<div class="row">' +
                            '<div class="col-sm-6 form-group mb-2">' +
                                '<label class="control-label"> <b> ARM Template File </b></label>' +
                                '<select class="form-control armTemplate" name="armTemplate-'+ serial +'" id="amTemplateFile-1">' + arm +
                                '</select>' +
                            '</div>' +

                            '<div class="col-sm-6 form-group mb-2">' +
                                '<label class="control-label"> <b>SubscripTion ID </b></label>' +
                                '<input type="text" class="form-control" name="subscriptionID-'+ serial +'">' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    '<div class="col-1"><i class="fa-solid fa-xmark mt-2 rConfig-xmarks" id="in-'+ serial +'-xmark"></i></div>' +
                '</div>';
        return html;
    }


 
    $(document.body).on("click" , ".rConfig-plus" , function(e) {
        serial++;
        $(".runnerConfig").append(getRunnerConfig( regions , templateFile , serial ))
    })

    $(document.body).on("click" , ".rConfig-xmarks" , function(e) {
        serial = this.id.split("-")[1];
        if (serial == '0') return;
        $("#instance-" + serial ).remove()
    })


    $(document.body).on('change',"#ARMTemplates",function (e) {
        template = $('#ARMTemplates :selected').val();
        templateFile = getARMfiles(ARMTemplates[template]);
        $(".armTemplate").empty();
        $(".armTemplate").append(templateFile)

     });


    
    $(".runnerConfig").append(getRunnerConfig( regions , templateFile, serial ))


    

});