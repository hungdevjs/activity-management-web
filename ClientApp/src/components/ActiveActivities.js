import React from "react"
import { Card, CardHeader, CardBody } from "reactstrap"

import ActivityList from "./ActivityList"

const ActiveActivities = ({
  activeActivities,
  signUp,
  attendance,
  attendanceCode,
  setAttendanceCode,
}) => {
  if (!activeActivities) return null

  return (
    <Card className="mb-3">
      <CardHeader>Active activities</CardHeader>
      <CardBody>
        {!!activeActivities.length ? (
          <ActivityList
            activities={activeActivities}
            signUp={signUp}
            attendance={attendance}
            attendanceCode={attendanceCode}
            setAttendanceCode={setAttendanceCode}
          />
        ) : (
          <p className="mb-0 text-center">No active activity.</p>
        )}
      </CardBody>
    </Card>
  )
}

export default ActiveActivities
