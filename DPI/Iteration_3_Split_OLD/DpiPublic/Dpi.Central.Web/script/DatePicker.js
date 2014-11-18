function UpdateDaysList (monthControl, dayControl, yearControl){        
    //if (monthControl.selectedIndex != 0 && yearControl.selectedIndex != 0){
        var SelectedMonth = monthControl[monthControl.selectedIndex].value;
        var SelectedYear = yearControl[yearControl.selectedIndex].value;        
        var DaysCount = GetDaysCount(SelectedMonth, SelectedYear);

        var DaysCountInTheControl = dayControl.length - 1;
        if (DaysCountInTheControl > DaysCount){
            for (var i = 0; i <(DaysCountInTheControl - DaysCount); i++){
                dayControl.options[dayControl.options.length - 1] = null;
            }
        }
        if (DaysCount > DaysCountInTheControl){
            for (var i = 0; i < (DaysCount - DaysCountInTheControl); i++){
                NewOption = new Option(dayControl.options.length);
                dayControl.add(NewOption);
            }
        }
        if (dayControl.selectedIndex < 0) dayControl.selectedIndex == 0;        
    //}
}

function GetDaysCount(month, year){
  var DaysCount = 31;
  if (month == 4 || month == 6 || month == 9 || month == 11) DaysCount = 30;
  if (month == 2 && (year/4) != Math.floor(year/4))	DaysCount = 28;
  if (month == 2 && (year/4) == Math.floor(year/4))	DaysCount = 29;
  return DaysCount;
}
