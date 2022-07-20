
$(document).ready(function () {

    const get_query_num = ( query ) => {
        const array = query.split("-");
        return parseInt(array[1]);
    }

    let dpValidation = 0;

    const getRunnerStage = (position) => {
        return '<div class="form-group row" id="rst-'+ position +'">'+
                    '<div class="col-1 dp-plus text-right"><i class="fa-solid fa-plus mt-2"></i></div>'+
                    '<div class="col-2 text-left"><label class="control-label"><b>Runner Stage </b></label></div>'+
                    '<div class="col-8"><input type="text" class="form-control" name="runnerStage-'+ position +'"></div>'+
                    '<div class="col-1"><i class="fa-solid fa-xmark mt-2 dp-xmark" id="dp-'+ position +'-xmark"></i></div>'+
                '</div>';
    }

    const getRunnerActivity = (parent, position ) => {

        return '<div class="form-group row mt-2" id="racccc-'+ parent +'-child-'+ position +'">' +
                    '<div class="col-3"></div>' +
                    '<div class="col-7">' +
                    '<div class="row">' +
                        '<div class="col-1 text-center"><i class="fa-solid fa-plus mt-2 dpa-plus parent-'+ parent +'" id="p-'+ parent +'-child-'+ position +'"></i></div>' +
                        '<div class="col-3 text-center"><label class="control-label">Runner Activiity</label></div>' +
                        '<div class="col-7"><input type="text" class="form-control" name="runnerActivity-'+ parent +'-child-'+ position +'"></div>' +
                        '<div class="col-1"><i class="fa-solid fa-xmark mt-2 dpa-xmark" id="dp-'+ parent +'-child-'+ position +'-xmark"></i></div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-2"></div>' +
                '</div>';
    }

    $(document.body).on("click" , ".dp-plus" , function(e) {
        dpValidation++;
        $(".dpValidation").append('<div class="dp-container stage-'+ dpValidation +' mb-3">'+ 
                                    getRunnerStage(dpValidation ) + 
                                    getRunnerActivity(dpValidation,0) +
                                '</div');
        
    })

    $(document.body).on("click" , ".dp-xmark" , function() {
        position = get_query_num(this.id);
        if (position == '0') return ;
        $(".stage-" + position ).remove();
        
    })
    

    $(".dpValidation").append('<div class="dp-container stage-'+ dpValidation +' mb-3">'+ 
                                    getRunnerStage(dpValidation ) + 
                                    getRunnerActivity(dpValidation,0) +
                                '</div');

    $(document.body).on("click" , ".dpa-plus" , function(e) {

        const array = this.id.split("-");
        let parent = array[1]
        console.log(".parent-"+ parent)
        // Check How many runner activity
        const runnerAc = $(".parent-"+ parent )

        var newPosition = runnerAc[runnerAc.length - 1].id.split('-')
        newPosition = parseInt(newPosition[3]) + 1;
        console.log(newPosition);

        $(".stage-"+ parent ).append( getRunnerActivity(parent,newPosition) )

    })

    $(document.body).on("click" , ".dpa-xmark" , function(e) {

        const array = this.id.split("-");
        let parent = array[1]
        let child = array[3]
        if ( child == '0' ) return;

        console.log("rac-" + parent + "-child-" + child)
        $("#racccc-" + parent + "-child-" + child ).remove()
    })

});