# Steps to run the server:
# 
# 1. Install python - https://www.python.org/downloads/
# 2. Run this server using the following command - `python exportServer.py`
#
# Now you can save the document to this server using `localhost:5000` as server path.

import http.server
import socketserver
import os

# Set the path to the folder where the files will be uploaded
UPLOADS_FOLDER = "Server"

# Define the port for the server
PORT = 5000

# Handler to handle file uploads
class FileUploadHandler(http.server.SimpleHTTPRequestHandler):
    def do_POST(self):
        # Get the uploaded file data
        content_length = int(self.headers['Content-Length'])
        file_data = self.rfile.read(content_length)

        # Get the file name from the Content-Disposition header
        content_disposition = self.headers.get('Content-Disposition', '')
        filename = content_disposition.split('filename=')[1].strip('"') if 'filename=' in content_disposition else 'uploaded_file.pdf'

        # Write the file to the specified folder
        with open(filename, 'wb') as f:
            f.write(file_data)

        # Respond with a success message
        self.send_response(200)
        self.end_headers()
        self.wfile.write(b'File uploaded successfully.')

if __name__ == "__main__":
    if not os.path.exists(UPLOADS_FOLDER):
        os.makedirs(UPLOADS_FOLDER)

    # Change the current directory to the uploads folder
    os.chdir(UPLOADS_FOLDER)

    # Start the server
    with socketserver.TCPServer(("", PORT), FileUploadHandler) as httpd:
        print(f"Serving at port {PORT}")
        httpd.serve_forever()
