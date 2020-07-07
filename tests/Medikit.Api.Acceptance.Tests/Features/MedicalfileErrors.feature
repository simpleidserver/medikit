Feature: MedicalfileErrors

Scenario: Check error is returned when medical file doesn't exist
	When execute HTTP GET request 'http://localhost/medicalfiles/invalidid'
	And extract JSON from body
	Then HTTP status code equals to '404'

Scenario: Check error is returned when trying to add medical file to unknown patient
	When execute HTTP POST JSON request 'http://localhost/medicalfiles'
	| Key        | Value   |
	| patient_id | invalid |	
	And extract JSON from body
	Then HTTP status code equals to '404'
	Then JSON 'errors.parameter[0]'='Unknown patient invalid'
	Then JSON 'status'='404'

Scenario: Check error is returned when trying to add medical file twice
	When execute HTTP POST JSON request 'http://localhost/patients'
	| Key            | Value                                                                      |
	| firstname      | firstname                                                                  |
	| lastname       | lastname                                                                   |
	| niss           | niss                                                                       |
	| gender         | 1                                                                          |
	| eid_cardnumber | eidcardnumber                                                              |
	| address        | { street: 'street', street_number: 2, postal_code: '1000', country: 'BE' } |
	| contact_infos  | [ { type: 0, value : 'toto@mail.com' } ]                                   |
	And extract JSON from body
	And extract 'id' from JSON body into 'id'
	And execute HTTP POST JSON request 'http://localhost/medicalfiles'
	| Key        | Value |
	| patient_id | $id$  |
	And execute HTTP POST JSON request 'http://localhost/medicalfiles'
	| Key        | Value |
	| patient_id | $id$  |
	And extract JSON from body
	Then HTTP status code equals to '400'
	Then JSON 'errors.parameter[0]'='Medical file already exists for the patient'
	Then JSON 'status'='400'
