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
import { requestForgetPassword } from "../services/accountService"
import { AppContext } from "../contexts/app.context"

const ForgetPassword = () => {
  const history = useHistory()
  const { setLoading } = useContext(AppContext)
  const [email, setEmail] = useState("")

  useAuthRoutes()

  const forgetPassword = async () => {
    try {
      if (!email || !email.trim()) throw new Error("Email is invalid")
      setLoading(true)
      await requestForgetPassword(email)
      history.push(`/verify-forget-password`, { email })
      toast.success("Please check your email to get security code!")
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
                <h3>Forget password</h3>
                <p className="text-muted">Your email</p>
                <InputGroup className="mb-3">
                  <Input
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                  />
                </InputGroup>
                <Row>
                  <Col md="12">
                    <Button
                      color="primary"
                      size="sm"
                      className="px-4"
                      block
                      onClick={forgetPassword}
                    >
                      ForgetPassword
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

export default ForgetPassword
