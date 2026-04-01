Feature: Invoices
    # https://github.com/karatelabs/karate/issues/1191
    # https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

  Background:
    * header Content-Type = 'application/json'

  Scenario: Happy Path

    * def jsUtils = read('./js-utils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authSlytherineTenantDracoLoginWithAllPermissions = jsUtils().getEnvVariable('AUTH_SLYTHERINE_TENANT_DRACO_MALFOY_LOGIN_WITH_ALL_PERMISSIONS')
    * def authSlytherineTenantDracoPasswordWithAllPermissions = jsUtils().getEnvVariable('AUTH_SLYTHERINE_TENANT_DRACO_MALFOY_PASSWORD_WITH_ALL_PERMISSIONS')

    # Authentication
    Given url authApiRootUrl
    And path '/login'
    And request
    """
    {
      "login": "#(authSlytherineTenantDracoLoginWithAllPermissions)",
      "password": "#(authSlytherineTenantDracoPasswordWithAllPermissions)"
    }
    """
    And method POST
    Then status 200

    * def accessToken = karate.toMap(response.accessToken.value)

    * configure headers = jsUtils().getAuthHeaders(accessToken)

    # Get all projects
    Given url apiRootUrl
    Given path 'invoices/projects'
    When method GET
    Then status 200
    And assert response.projects.length > 0

    # Get employee's tracked task hours
    # We specifically substitute 1991 year for which there is no exact data
    Given url apiRootUrl
    Given path 'invoices/employees-tracked-task-hours'
    And params { year: "1991", month: "3", projectId: "2" }
    When method GET
    Then status 200
    And assert response.EmployeesTrackedTaskHours.length = 0