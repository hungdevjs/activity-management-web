import React, { useState, useContext, useEffect } from "react"
import { toast } from "react-toastify"
import { Row, Col } from "reactstrap"

import Semester from "../components/Semester"
import Profile from "../components/Profile"
import Score from "../components/Score"
import ActiveActivities from "../components/ActiveActivities"

import { AppContext } from "../contexts/app.context"
import {
  getSemester,
  getScore,
  getActiveActivities,
  signUpActivity,
  attendanceActivity,
  updateStatusActivity,
} from "../services/activityService"
import { getProfile } from "../services/studentService"

const Dashboard = () => {
  const { setLoading } = useContext(AppContext)
  const [semester, setSemester] = useState(null)
  const [score, setScore] = useState(null)
  const [activeActivities, setActiveActivities] = useState([])
  const [profile, setProfile] = useState(null)
  const [attendanceCode, setAttendanceCode] = useState("")

  const getData = async () => {
    setLoading(true)

    try {
      await updateStatusActivity()
      const [semesterRes, scoreRes, activeActivitiesRes, profileRes] =
        await Promise.all([
          getSemester(),
          getScore(),
          getActiveActivities(),
          getProfile(),
        ])

      setSemester(semesterRes.data)
      setScore(scoreRes.data)
      setActiveActivities(activeActivitiesRes.data)
      setProfile(profileRes.data)
    } catch (err) {
      toast.error(err.response?.data || err.message)
    }

    setLoading(false)
  }

  useEffect(() => {
    getData()
  }, [])

  const signUp = async (id) => {
    setLoading(true)

    try {
      await signUpActivity(id)
      const res = await getActiveActivities()
      setActiveActivities(res.data)
      toast.success("Signing up successfully!")
    } catch (err) {
      toast.error(err.response?.data || err.message)
    }

    setLoading(false)
  }

  const attendance = async (id) => {
    setLoading(true)

    try {
      if (!attendanceCode || !attendanceCode.trim())
        throw new Error("Attendance code is empty")
      await attendanceActivity(id, attendanceCode)
      const [activeActivitiesRes, scoreRes] = await Promise.all([
        getActiveActivities(),
        getScore(),
      ])
      setActiveActivities(activeActivitiesRes.data)
      setScore(scoreRes.data)
      setAttendanceCode("")
      toast.success("Attendance successfully!")
    } catch (err) {
      toast.error(err.response?.data || err.message)
    }

    setLoading(false)
  }

  return (
    <Row>
      <Col md={4}>
        <Semester semester={semester} />
        <Profile profile={profile} />
      </Col>
      <Col md={8}>
        <Score score={score} />
        <ActiveActivities
          activeActivities={activeActivities}
          signUp={signUp}
          attendance={attendance}
          attendanceCode={attendanceCode}
          setAttendanceCode={setAttendanceCode}
        />
      </Col>
    </Row>
  )
}

export default Dashboard
