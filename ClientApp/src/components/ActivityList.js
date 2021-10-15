import React, { useState } from "react"
import { ListGroup, ListGroupItem, Collapse } from "reactstrap"
import { BiRightArrow, BiDownArrow } from "react-icons/bi"
import moment from "moment"

import { DATE_FORMAT, ACTIVITY_STATUS } from "../utils/constants"

const generateTextColor = (status) => {
  if (status === ACTIVITY_STATUS.Absence) return "#e74c3c"
  if (status === ACTIVITY_STATUS.Attendance) return "#2ecc71"
  if (status === ACTIVITY_STATUS.Pending) return "#f1c40f"
  if (status === ACTIVITY_STATUS.Approved) return "#3498db"
  if (status === ACTIVITY_STATUS.Cancelled) return "#34495e"
}

const generateText = (activity) => {
  switch (activity.status) {
    case ACTIVITY_STATUS.Absence:
      return `-${activity.minusPoint} point(s)`
    case ACTIVITY_STATUS.Attendance:
      return `+${activity.plusPoint} point(s)`
    case ACTIVITY_STATUS.Approved:
      return "Approved"
    case ACTIVITY_STATUS.Pending:
      return "Pending"
    case ACTIVITY_STATUS.Cancelled:
      return "Cancelled"
  }
}

const ActivityList = ({ activities, isList }) => {
  const [openingIds, setOpeningIds] = useState([])

  const toggle = (id) => {
    if (openingIds.includes(id)) {
      setOpeningIds(openingIds.filter((item) => item !== id))
      return
    }

    setOpeningIds([...openingIds, id])
  }

  const style = { maxHeight: isList ? "80vh" : "50vh" }

  return (
    <ListGroup className="overflow-auto" style={style}>
      {activities.map((activity) => {
        const isOpen = openingIds.includes(activity.id)
        return (
          <ListGroupItem key={activity.id}>
            <div
              className={`d-flex ${
                isList && "flex-column"
              } align-items-center justify-content-between`}
            >
              {isList && (
                <b style={{ color: generateTextColor(activity.status) }}>
                  {generateText(activity)}
                </b>
              )}{" "}
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
              <p className="mb-1">Max students: {activity.numberOfStudents}</p>
              <p className="mb-1">Activity type: {activity.activityTypeName}</p>
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
  )
}

export default ActivityList
