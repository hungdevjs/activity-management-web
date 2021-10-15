import React, { useEffect, useState, useContext } from "react"
import { Input, Label, FormGroup, Form, Button } from "reactstrap"
import { toast } from "react-toastify"

import { AppContext } from "../contexts/app.context"
import { getProfile, updateProfile } from "../services/studentService"

const inputs = [
  {
    name: "Student code",
    field: "studentCode",
    isDisabled: true,
  },
  {
    name: "Email",
    field: "email",
    isDisabled: true,
  },
  {
    name: "Class",
    field: "className",
    isDisabled: true,
  },
  {
    name: "First name",
    field: "firstName",
  },
  {
    name: "Last name",
    field: "lastName",
  },
  {
    name: "Phone number",
    field: "phoneNumber",
  },
  {
    name: "Address",
    field: "address",
  },
]

const Profile = () => {
  const { setLoading, setUser } = useContext(AppContext)
  const [profile, setProfile] = useState(null)

  const changeProfile = (valueObject) =>
    setProfile({
      ...profile,
      ...valueObject,
    })

  const getData = async () => {
    setLoading(true)

    try {
      const res = await getProfile()
      setProfile(res.data)
    } catch (err) {
      toast.error(err.message)
    }

    setLoading(false)
  }

  useEffect(() => {
    getData()
  }, [])

  const submit = async (e) => {
    setLoading(true)

    try {
      e.preventDefault()
      if (!profile.firstName || !profile.firstName.trim())
        throw new Error("First name is empty")
      if (!profile.lastName || !profile.lastName.trim())
        throw new Error("Last name is empty")
      if (!profile.phoneNumber || !profile.phoneNumber.trim())
        throw new Error("Phone number is empty")
      if (!profile.address || !profile.address.trim())
        throw new Error("Address is empty")

      const { id, firstName, lastName, phoneNumber, address } = profile
      const res = await updateProfile({
        id,
        firstName,
        lastName,
        phoneNumber,
        address,
      })
      setUser(res.data)
      toast.success("Update profile successfully")
    } catch (err) {
      toast.error(err.message)
    }

    setLoading(false)
  }

  if (!profile) return null

  return (
    <Form onSubmit={submit}>
      {inputs.map((input) => (
        <FormGroup key={input.name} className="mb-2">
          <Label>{input.name}</Label>
          <Input
            disabled={!!input.isDisabled}
            value={profile[input.field]}
            onChange={(e) => changeProfile({ [input.field]: e.target.value })}
          />
        </FormGroup>
      ))}
      <Button color="success" block>
        Save
      </Button>
    </Form>
  )
}

export default Profile
