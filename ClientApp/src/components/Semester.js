import React from "react"
import { Card, CardHeader, CardBody } from "reactstrap"
import moment from "moment"

import { DATE_FORMAT } from "../utils/constants"

const Semester = ({ semester }) => {
  if (!semester) return null

  return (
    <Card className="mb-3">
      <CardHeader>Semester</CardHeader>
      <CardBody>
        <p>Name: {semester.name}</p>
        <p>Start time: {moment(semester.startTime).format(DATE_FORMAT)}</p>
        <p>End time: {moment(semester.endTime).format(DATE_FORMAT)}</p>
        <p>Year: {semester.yearName}</p>
      </CardBody>
    </Card>
  )
}

export default Semester
