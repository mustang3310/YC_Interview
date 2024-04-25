FROM debian:12.2

# Add the script to the Docker Image
# ADD /crontab/renew-script.sh /root/renew-script.sh
# RUN chmod 0644 /root/renew-script.sh

# ADD /crontab/certbot-install-cert.sh /root/certbot-install-cert.sh
# RUN chmod 0644 /root/certbot-install-cert.sh

RUN apt-get update
RUN apt -y install certbot
# RUN apt-get -y install nano

# WORKDIR /tmp/acme-challenge
# WORKDIR /root/log
# RUN chmod 0644 /root/log 
WORKDIR /root/acme-challenge/
WORKDIR /root

ENTRYPOINT ["tail", "-f", "/dev/null"]
