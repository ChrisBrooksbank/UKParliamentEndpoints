# UKParliamentEndpoints

This project maintains a collection of public UK Parliament API endpoints. 
The data is stored in Azure Table storage, simplifying the discovery and use of public UK Parliament data.

## Endpoints

### Parliament Endpoints

1. **GET /api/ParliamentEndpoint**
   - **Description**: Retrieves a list of all stored Parliament API endpoints.
   - **Response**: JSON array of endpoint objects.

2. **GET /api/ParliamentEndpoint/{id}**
   - **Description**: Retrieves a specific Parliament API endpoint by its ID.
   - **Parameters**: `id` (string) - The ID of the endpoint.
   - **Response**: JSON object of the endpoint details.

3. **POST /api/ParliamentEndpoint**
   - **Description**: Adds a new Parliament API endpoint.
   - **Request Body**: JSON object with endpoint details.
   - **Response**: JSON object of the created endpoint.

4. **PUT /api/ParliamentEndpoint/{id}**
   - **Description**: Updates an existing Parliament API endpoint by its ID.
   - **Parameters**: `id` (string) - The ID of the endpoint.
   - **Request Body**: JSON object with updated endpoint details.
   - **Response**: JSON object of the updated endpoint.

5. **DELETE /api/ParliamentEndpoint/{id}**
   - **Description**: Deletes a specific Parliament API endpoint by its ID.
   - **Parameters**: `id` (string) - The ID of the endpoint.
   - **Response**: Status message indicating the result of the operation.

## Configuration

To run this project, you need to configure your Azure Table connection string.

1. Open the configuration file.
2. Add your Azure Table connection string in the appropriate section.

## Usage

1. Clone the repository: `git clone https://github.com/ChrisBrooksbank/UKParliamentEndpoints.git`
2. Navigate to the project directory: `cd UKParliamentEndpoints`
3. Configure your Azure Table connection string.
4. Build and run the project.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
