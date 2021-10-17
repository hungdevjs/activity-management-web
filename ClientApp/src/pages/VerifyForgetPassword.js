import React, { useState, useContext, useEffect } from "react"
import { useHistory, useLocation } from "react-router-dom"
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
import { verifyForgetPassword } from "../services/accountService"
import { AppContext } from "../contexts/app.context"

const VerifyForgetPassword = () => {
  const history = useHistory()
  const location = useLocation()
  const { setLoading } = useContext(AppContext)
  const [code, setCode] = useState("")
  const [newPassword, setNewPassword] = useState("")

  useAuthRoutes()

  const { email } = location.state || {}

  useEffect(() => {
    if (!email) history.push("/login")
  }, [])

  const verify = async () => {
    try {
      if (!code || !code.trim()) throw new Error("Email is invalid")
      if (!newPassword || !newPassword.trim())
        throw new Error("Password is invalid")
      setLoading(true)
      await verifyForgetPassword({
        email,
        newPassword,
        code,
      })
      history.push("/login")
      toast.success("Reset password successfully!")
    } catch (err) {
      toast.error(err.response?.data || err.message)
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
                <p className="text-muted">Reset your password</p>
                <InputGroup className="mb-3">
                  <Input
                    placeholder="Security code"
                    value={code}
                    onChange={(e) => setCode(e.target.value)}
                  />
                </InputGroup>
                <InputGroup className="mb-4">
                  <Input
                    type="password"
                    placeholder="New password"
                    value={newPassword}
                    onChange={(e) => setNewPassword(e.target.value)}
                  />
                </InputGroup>
                <Row>
                  <Col md="12">
                    <Button
                      color="primary"
                      className="px-4"
                      block
                      onClick={verify}
                    >
                      Reset your password
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

export default VerifyForgetPassword
