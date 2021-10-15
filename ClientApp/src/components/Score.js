import React from "react"
import { Card, CardHeader, CardBody } from "reactstrap"

const Score = ({ score }) => {
  if (!score) return null
  
  return <Card className="mb-3">
    <CardHeader className="text-center">Activity point</CardHeader>
    <CardBody>
      <p className="mb-0 text-center font-weight-bold h3">{score}</p>
    </CardBody>
  </Card>
}

export default Score