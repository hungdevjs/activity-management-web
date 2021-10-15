import React from "react"
import { Card, CardHeader, CardBody } from "reactstrap"

const Profile = ({ profile }) => {
  if (!profile) return null

  return (
    <Card className="mb-3">
      <CardHeader>Student profile</CardHeader>
      <CardBody>
        <p>
          Name: {profile.firstName} {profile.lastName}
        </p>
        <p>Email: {profile.email}</p>
        <p>Phone number: {profile.phoneNumber}</p>
        <p>Address: {profile.address}</p>
        <p>Student ID: {profile.studentCode}</p>
        <p>Class: {profile.className}</p>
      </CardBody>
    </Card>
  )
}

export default Profile
