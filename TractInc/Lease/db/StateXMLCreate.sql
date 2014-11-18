select state.*, county.* 
  from county 
    inner join state on state.stateid = county.stateid
  order by state.[name], county.[name] 
  for xml auto