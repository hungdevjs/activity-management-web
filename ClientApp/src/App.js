import React from "react"
import { ToastContainer } from "react-toastify"
import { BrowserRouter as Router, Route, Switch } from "react-router-dom"

import "react-toastify/dist/ReactToastify.css"
import "./custom.css"

import Login from "./pages/Login"
import ForgetPassword from "./pages/ForgetPassword"
import VerifyForgetPassword from "./pages/VerifyForgetPassword"
import MainLayout from "./layout/MainLayout"
import Loading from "./components/Loading"
import AppContextProvider from "./contexts/app.context"

const App = () => {
  return (
    <AppContextProvider>
      <ToastContainer
        position="top-right"
        autoClose={2500}
        hideProgressBar
        newestOnTop={false}
        closeOnClick
        rtl={false}
        draggable
      />
      <Loading />
      <Router>
        <Switch>
          <Route path="/login" exact component={Login} />
          <Route path="/forget-password" exact component={ForgetPassword} />
          <Route
            path="/verify-forget-password"
            exact
            component={VerifyForgetPassword}
          />
          <Route path="/" component={MainLayout} />
        </Switch>
      </Router>
    </AppContextProvider>
  )
}

export default App
