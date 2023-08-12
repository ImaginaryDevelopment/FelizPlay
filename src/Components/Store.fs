module Components.Store
// import { configureStore } from "@reduxjs/toolkit";

type UserInterface = {
  AccountType: string
  CanChangePassword: bool
  CanEditProfile: bool
  DisplayName: string
  Email: string
  // ExternalPasswordChangeUrl?
  ExternalPasswordChangeUrl: string option
  // ExternalProfileUrl?
  ExternalProfileUrl: string option
  FirstName: string
  HasDevices: bool
  HasExternalPasswordChangeUrl: bool
  HasExternalProfileUrl: bool
  HasJobDescription: bool
  HasStudentContactInfo: bool
  HasStudentDevices: bool
  HasStudentInfo: bool
  JobDescription: string
  LastName: string
  MyDeviceCount: int
  ObjectID: string
  PhotoUrl: obj
  StudentContactCount: int
  StudentCount: int
  StudentDeviceCount: int
  StudentIds: obj[]
  UserPrincipalName: string
  dcpsAUPAcceptanceDate: string
  dcpsID: string
  dcpsType: string
  extranetGUID: string
}

type UserState = {
    user: UserInterface
}

module UserSlice = 
    let initialState : UserState = {
        user = {
            AccountType = ""
            CanChangePassword = false
            CanEditProfile = false
            DisplayName = ""
            Email = ""
            ExternalPasswordChangeUrl = None
            ExternalProfileUrl = None
            FirstName = ""
            HasDevices = false
            HasExternalPasswordChangeUrl = false
            HasExternalProfileUrl = false
            HasJobDescription = false
            HasStudentContactInfo = false
            HasStudentDevices = false
            HasStudentInfo = false
            JobDescription = ""
            LastName = ""
            MyDeviceCount = 0
            ObjectID = ""
            PhotoUrl = ""
            StudentContactCount = 0
            StudentCount = 0
            StudentDeviceCount = 0
            StudentIds = Array.empty
            UserPrincipalName = ""
            dcpsAUPAcceptanceDate = ""
            dcpsID = ""
            dcpsType = ""
            extranetGUID = ""
        }
    }
module internal Impl = // https://github.com/fable-compiler/fable-browser/tree/master/src/WebStorage
    let localStorage = Browser.WebStorage.localStorage
    let inline deserialize<'t>(text: string) =
        Fable.Core.JS.JSON.parse(text) :?> 't
    
open Impl
// https://github.com/ImaginaryDevelopment/SkyblockHelper/blob/master/src/Client/CodeHelpers/BrowserStorage.fs
type Internal =
    static member inline TryGet< 't when 't : equality > (key) : 't option =
        localStorage.getItem key
        |> Option.ofObj
        |> Option.bind (fun x ->
            let result: 't option = 
                let v = deserialize x
                if System.Object.ReferenceEquals(v,null) then
                    None
                else Some v
            result
                
        )

// a store with hooks that determine how to update state
//let store = 