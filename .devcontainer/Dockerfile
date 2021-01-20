
FROM mcr.microsoft.com/vscode/devcontainers/base:0-focal

RUN rm /bin/sh && ln -s /bin/bash /bin/sh

USER vscode

# Install dotnet
WORKDIR /dotnet-install
RUN sudo wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && sudo dpkg -i packages-microsoft-prod.deb

RUN sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-5.0

# Install node
WORKDIR /node-install
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.35.3/install.sh | bash \
  && source ~/.bashrc \
  && nvm install 14.15.1

# Install yarn
WORKDIR /yarn-install
RUN curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | sudo apt-key add - \
    && echo "deb https://dl.yarnpkg.com/debian/ stable main" | sudo tee /etc/apt/sources.list.d/yarn.list
RUN sudo apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && sudo apt-get -y install --no-install-recommends yarn
