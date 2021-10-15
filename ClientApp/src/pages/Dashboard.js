import React, { useState, useContext, useEffect } from "react"
import { toast } from "react-toastify"
import { Row, Col } from "reactstrap"

import Semester from "../components/Semester"
import Profile from "../components/Profile"
import Score from "../components/Score"
import ActiveActivities from "../components/ActiveActivities"

import { AppContext } from "../contexts/app.context"
import { getSemester, getScore, getActiveActivities } from "../services/activityService"
import { getProfile } from "../services/studentService"

const Dashboard = () => {
  const { setLoading } = useContext(AppContext)
  const [semester, setSemester] = useState(null)
  const [score, setScore] = useState(null)
  const [activeActivities, setActiveActivities] = useState([])
  const [profile, setProfile] = useState(null)

  const getData = async () => {
    setLoading(true)

    try {
      const [semesterRes, scoreRes, activeActivitiesRes, profileRes] = await Promise.all([
        getSemester(),
        getScore(),
        getActiveActivities(),
        getProfile()
      ])

      setSemester(semesterRes.data)
      setScore(scoreRes.data)
      setActiveActivities(activeActivitiesRes.data)
      setProfile(profileRes.data)
    } catch(err) {
      toast.error(err.message)
    }

    setLoading(false)
  }

  useEffect(() => {
    getData()
  }, [])

  return <Row>
    <Col md={4}>
      <Semester semester={semester} />
      <Profile profile={profile} />
    </Col>
    <Col md={8}>
      <Score score={score} />
      <ActiveActivities activeActivities={activeActivities} />
    </Col>
  </Row>
}

export default Dashboard
