import http.server
import socketserver
import os


class RequestHandler(http.server.SimpleHTTPRequestHandler):
    def do_GET(self):
        if self.path == '/stopmitmdump':
            # Kill the program here
            os.system("pkill mitmdump")
            self.send_response(200)
            self.end_headers()
        else:
            # Serve files as usual
            super().do_GET()


with socketserver.TCPServer(("", 3000), RequestHandler) as httpd:
    httpd.serve_forever()
