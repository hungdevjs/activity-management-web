import React, { useState } from "react"
import {
  Card,
  CardHeader,
  CardBody,
  ListGroup,
  ListGroupItem,
  Collapse,
} from "reactstrap"
import { BiRightArrow, BiDownArrow } from "react-icons/bi"
import moment from "moment"

import { DATE_FORMAT } from "../utils/constants"

const ActiveActivities = ({ activeActivities }) => {
  const [openingIds, setOpeningIds] = useState([])

  const toggle = (id) => {
    if (openingIds.includes(id)) {
      setOpeningIds(openingIds.filter((item) => item !== id))
      return
    }

    setOpeningIds([...openingIds, id])
  }

  if (!activeActivities) return null

  return (
    <Card className="mb-3">
      <CardHeader>Active activities</CardHeader>
      <CardBody>
        {!!activeActivities.length ? (
          <ListGroup className="overflow-auto list-activity">
            {activeActivities.map((activity) => {
              const isOpen = openingIds.includes(activity.id)
              return (
                <ListGroupItem key={activity.id}>
                  <div className="d-flex align-items-center justify-content-between">
                    <b>{activity.name}</b>
                    <div>
                      {moment(activity.startTime).format(DATE_FORMAT)} -{" "}
                      {moment(activity.endTime).format(DATE_FORMAT)}
                      <span className="ml-4">
                        {isOpen ? (
                          <BiDownArrow
                            className="cursor-pointer"
                            onClick={() => toggle(activity.id)}
                          />
                        ) : (
                          <BiRightArrow
                            className="cursor-pointer"
                            onClick={() => toggle(activity.id)}
                          />
                        )}
                      </span>
                    </div>
                  </div>
                  <Collapse isOpen={isOpen} className="mt-3">
                    <p className="mb-1">
                      <i>{activity.creatorName}</i>
                    </p>
                    <p className="mb-1">{activity.description}</p>
                    <p className="mb-1">
                      Max students: {activity.numberOfStudents}
                    </p>
                    <p className="mb-1">
                      Activity type: {activity.activityTypeName}
                    </p>
                    <p className="mb-1">Plus point: {activity.plusPoint}</p>
                    <p className="mb-1">Minus point: {activity.minusPoint}</p>
                    <p className="mb-0">
                      Signing up time:{" "}
                      {moment(activity.signUpStartTime).format(DATE_FORMAT)} -{" "}
                      {moment(activity.signUpEndTime).format(DATE_FORMAT)}
                    </p>
                  </Collapse>
                </ListGroupItem>
              )
            })}
          </ListGroup>
        ) : (
          <p className="mb-0 text-center">No active activity.</p>
        )}
      </CardBody>
    </Card>
  )
}

export default ActiveActivities
