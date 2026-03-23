Feature: Invoices Entries
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

    # Get employee's projects
    Given url apiRootUrl
    Given path 'invoices/projects'
    When method GET
    And match response.projects contains
    """
    {
      "id": 1,
      "name": "Project1"
    },
    {
      "id": 2,
      "name": "Project2"
    }
    """
    Then status 200

    # Get employee's time entries
    Given url apiRootUrl
    Given path 'invoices/employees-entries-by-project-and-period'
    And param year = 2026
    And param month = 3
    And param projectId = 2
    When method GET
    And match response.employeesEntries contains
    """
    {
      "id": 2,
      "name": "Name Name Name",
      "trackedHours": 0.5
    }
    """
    Then status 200
