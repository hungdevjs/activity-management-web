import React, { useContext, useEffect } from "react"
import { Switch, Route, useHistory } from "react-router-dom"

import ContentWithHeader from "./ContentWithHeader"
import routes from "../configs/routes"
import { AppContext } from "../contexts/app.context"
import { getAccessToken, isCurrentSemester } from "../utils/helpers"
import { ACCESS_TOKEN } from "../utils/constants"
import { getUserInfo } from "../services/accountService"
import { getAllSemesters } from "../services/activityService"

const MainLayout = () => {
  const history = useHistory()
  const { user, setUser, setSemesters, setSemesterId } = useContext(AppContext)
  const checkAuth = async () => {
    const token = getAccessToken()
    if (!token) {
      history.push("/login")
      return
    }

    try {
      const res = await getUserInfo(token)
      setUser(res.data)
      const semesterRes = await getAllSemesters()
      setSemesters(semesterRes.data)
      const currentSemester = semesterRes.data?.find((item) =>
        isCurrentSemester(item)
      )
      if (currentSemester) {
        setSemesterId(currentSemester.id)
      }
    } catch (err) {
      localStorage.removeItem(ACCESS_TOKEN)
      history.push("/login")
    }
  }

  useEffect(() => {
    checkAuth()
  }, [])

  if (!user) return null

  return (
    <ContentWithHeader>
      <Switch>
        {routes.map((route) => (
          <Route
            key={route.name}
            path={route.path}
            component={route.component}
            exact={route.exact}
          />
        ))}
      </Switch>
    </ContentWithHeader>
  )
}

export default MainLayout
