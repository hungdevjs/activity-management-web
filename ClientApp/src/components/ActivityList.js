import React, { useState } from "react"
import { ListGroup, ListGroupItem, Collapse } from "reactstrap"
import { BiRightArrow, BiDownArrow } from "react-icons/bi"
import moment from "moment"

import { DATE_FORMAT } from "../utils/constants"

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
