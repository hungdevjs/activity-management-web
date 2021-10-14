import React from "react"
import { Container } from "reactstrap"
import Header from "./Header"

const ContentWithHeader = ({ children }) => {
  return (
    <div>
      <Header />
      <Container>{children}</Container>
    </div>
  )
}

export default ContentWithHeader
