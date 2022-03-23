function checkBeforeSend() {

    const surveyForm = $("#SurveyBody");
    surveyForm.validate();
    $.validator.messages.required = 'Este campo no puede estar vacio!';
    if (surveyForm.valid()) {
        let dataColleted = surveyForm.serializeArray();
        let surveyId = dataColleted.find(x => x.name == "survey.Id").value;
        dataColleted = dataColleted.filter(x => x.name != "survey.Id" && x.name != "__RequestVerificationToken")


        let fieldValuesList = [];

        dataColleted.forEach(x => {
            fieldValuesList.push({"FieldId":x.name, "value":x.value})
        })

        let url = $("#SurveyBody").attr("action");
        
        saveData(url, surveyId, fieldValuesList);
    } 
   
   
}

function saveData(url, surveyId, data) {
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: {
            surveyId: JSON.stringify(surveyId),
            data: JSON.stringify(data)
        },
        cache: false,
        success: function (data) {
            if (data.success) {
                var anchor = document.createElement('a');
                anchor.href = data.url;
                //anchor.target = "_blank";
                anchor.click();
            }
        }
    })
}