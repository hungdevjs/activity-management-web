import React from "react"
import { Card, CardHeader, CardBody } from "reactstrap"

import ActivityList from "./ActivityList"

const ActiveActivities = ({ activeActivities }) => {
  if (!activeActivities) return null

  return (
    <Card className="mb-3">
      <CardHeader>Active activities</CardHeader>
      <CardBody>
        {!!activeActivities.length ? (
          <ActivityList activities={activeActivities} />
        ) : (
          <p className="mb-0 text-center">No active activity.</p>
        )}
      </CardBody>
    </Card>
  )
}

export default ActiveActivities
