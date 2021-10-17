import React, { useContext } from "react"
import Select from "react-select"

import { AppContext } from "../contexts/app.context"

const SemesterSelector = () => {
  const { semesters, selectedSemester, setSemesterId } = useContext(AppContext)

  const semesterOptions = semesters.map((item) => ({
    value: item.id,
    label: `${item.name} - ${item.yearName}`,
  }))

  const selectedOption = selectedSemester
    ? {
        value: selectedSemester.id,
        label: `${selectedSemester.name} - ${selectedSemester.yearName}`,
      }
    : null

  return (
    <Select
      options={semesterOptions}
      value={selectedOption}
      onChange={(e) => setSemesterId(e.value)}
    />
  )
}

export default SemesterSelector
