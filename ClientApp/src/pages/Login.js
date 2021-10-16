import React, { useState, useContext } from "react"
import { useHistory } from "react-router-dom"
import { toast } from "react-toastify"
import {
  Button,
  Card,
  CardBody,
  Col,
  Container,
  Input,
  InputGroup,
  Row,
} from "reactstrap"

import useAuthRoutes from "../hooks/useAuthRoutes"
import { logIn } from "../services/accountService"
import { AppContext } from "../contexts/app.context"
import { ACCESS_TOKEN } from "../utils/constants"

const Login = () => {
  const history = useHistory()
  const { setUser, setLoading } = useContext(AppContext)
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  useAuthRoutes()

  const login = async () => {
    try {
      if (!email || !email.trim()) throw new Error("Email is invalid")
      if (!password || !password.trim()) throw new Error("Password is invalid")
      setLoading(true)
      const res = await logIn({ email, password })
      const { data, token } = res.data
      localStorage.setItem(ACCESS_TOKEN, token)
      setUser(data)
      history.push("/")
      toast.success("Login successfully!")
    } catch (err) {
      toast.error(err.message)
    }
    setLoading(false)
  }

  return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center">
      <Container>
        <Row className="justify-content-center">
          <Col md="4">
            <Card className="p-4">
              <CardBody>
                <h1>Login</h1>
                <p className="text-muted">Sign In to your account</p>
                <InputGroup className="mb-3">
                  <Input
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                  />
                </InputGroup>
                <InputGroup className="mb-4">
                  <Input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  />
                </InputGroup>
                <Row>
                  <Col xs="4">
                    <Button color="primary" className="px-4" onClick={login}>
                      Login
                    </Button>
                  </Col>
                  <Col xs="8" className="text-right">
                    <Button
                      color="link"
                      className="px-0"
                      onClick={() => history.push("/forget-password")}
                    >
                      Forgot password?
                    </Button>
                  </Col>
                </Row>
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  )
}

export default Login
