import paramiko
ssh = 
try:
    ssh.connect(ip, username=user, key_filename=key_file)
    return True
except (BadHostKeyException, AuthenticationException, 
        SSHException, socket.error) as e:
    print e
    sleep(interval)
