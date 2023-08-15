module Components.Store.UserSlice

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

let userSlice: StoreSchema.Reducer<UserState> = 
    StoreSchema.Imports.createSlice {|
        name= "user"
        initialState= initialState
        reducers= {|
            updateUser= fun (state, action:Components.Store.StoreSchema.PayloadAction<UserState>) ->
                Fable.Core.JS.Constructors.Object.assign(state, {| user= action.payload.user |})
        |}
    |}