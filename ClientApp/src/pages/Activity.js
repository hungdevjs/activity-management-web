import React, { useState, useEffect, useContext } from "react"
import { toast } from "react-toastify"
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap"

import ActivityList from "../components/ActivityList"

import { AppContext } from "../contexts/app.context"
import {
  getActivities,
  updateStatusActivity,
} from "../services/activityService"

const Activity = () => {
  const { setLoading } = useContext(AppContext)
  const [active, setActive] = useState([])
  const [passed, setPassed] = useState([])
  const [upcoming, setUpcoming] = useState([])

  const getData = async () => {
    setLoading(true)

    try {
      await updateStatusActivity()
      const res = await getActivities()
      setActive(res.data?.active)
      setPassed(res.data?.passed)
      setUpcoming(res.data?.upcoming)
    } catch (err) {
      toast.error(err.message)
    }

    setLoading(false)
  }

  useEffect(() => {
    getData()
  }, [])

  const cols = [
    {
      name: "Passed activities",
      data: passed,
    },
    {
      name: "Active activities",
      data: active,
    },
    {
      name: "Upcoming activities",
      data: upcoming,
    },
  ]

  return (
    <Row>
      {cols.map((col) => (
        <Col md={4} key={col.name}>
          <Card>
            <CardHeader>{col.name}</CardHeader>
            <CardBody>
              {!!col.data.length ? (
                <ActivityList activities={col.data} isList />
              ) : (
                <p className="text-center">
                  You have no {col.name.toLowerCase()} on this semester.
                </p>
              )}
            </CardBody>
          </Card>
        </Col>
      ))}
    </Row>
  )
}

export default Activity
