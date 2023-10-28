# Dummy Credentials for login 
# For Admin:
## mobil: [94719352]  pass: [Icardi1990!]
# For Ansatt:
## mobil: [94717177]  pass: [Icardi1990!]

# Demonstration of the app:
![IGLAND WINCH](https://github.com/bxn0/ourWinch/assets/82652466/ee33a86f-190b-4c80-8056-2b5fe61da9a5)

# ourWinch
  - **Dashboard**
    - ActiveServiceController
    - CompletedController
    - DashboardController
    - NewServiceController
    - ServiceManagerController
    - ServiceOrderController
  - **Data**
    - AppDbContext
    - Migrations
  - **Models**
    - **Account**
      - ForgotPasswordViewModel
      - LoginViewModel
      - ResetPasswordViewModel
      - UsageOperationsModel
    - **ChecklistModel**
      - Electro
      - FunctionalTest
      - Hydraulic
      - Mechanical
      - Pressure
    - **DashboardModel**
      - ActiveService
      - DashboardModel
      - ServiceOrder
      - ErrorViewModel
  - **Views**
    - **Account**
      - ForgotPassword
      - Login
      - ResetPassword
    - **Completed**
      - Completed.cshtml
    - **Dashboard**
      - Index.cshtml
      - NewService.cshtml
    - **Electro**
      - Create
      - Delete
      - Details
      - Edit
      - Index
    - **FunctionalTest**
      - Create
      - Delete
      - Details
      - Edit
      - Index
    - **Hydraulic**
      - Create
      - Delete
      - Details
      - Edit
      - Index
    - **Mechanical**
      - Create
      - Delete
      - Details
      - Edit
      - Index
    - **Pressure**
      - Create
      - Delete
      - Details
      - Edit
      - Index
  - **Root**
    - **Css**
      - Account-css
      - Dashboard-css
      - ServiceOrder-css
    - **Program.cs**
      - Program
      - Startup
    - **Shared**
      - Layout.cshtml
