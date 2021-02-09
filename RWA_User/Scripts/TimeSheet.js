function valueChangedEvent() {
    var elementWorkingHours = document.getElementsByClassName("txtBoxWH");
    var elementOverTimeHours = document.getElementsByClassName("txtBoxOH");
    var workingHours=0;
    var overTimeHours = 0;
    for (var i = 0; i < elementWorkingHours.length; i++) {
        workingHours += parseInt(elementWorkingHours[i].value);
    }
    for (var i = 0; i < elementOverTimeHours.length; i++) {
        overTimeHours += parseInt(elementOverTimeHours[i].value);
    }
    workingHours = workingHours / 2;
    overTimeHours = overTimeHours / 2;
    //ovdje još treba doći koda
    //console.log(`Working hours: ${workingHours}`)
    //console.log(`Overtime hours: ${overTimeHours}`)
    //console.log(`Day: ${tsDate}`)
    //console.log(`Employee Type: ${employeeType}`)
}
//if (tsDate) {

//}

