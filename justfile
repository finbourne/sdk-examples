test-csharp:
    docker run -t \
        -e FBN_LUSID_API_URL=${FBN_LUSID_API_URL} \
        -e FBN_TOKEN_URL=${FBN_TOKEN_URL} \
        -e FBN_ACCESS_TOKEN=${FBN_ACCESS_TOKEN} \
        -e FBN_USERNAME=${FBN_USERNAME} \
        -e FBN_CLIENT_ID=${FBN_CLIENT_ID} \
        -e FBN_CLIENT_SECRET=${FBN_CLIENT_SECRET} \
        -e FBN_LUSID_API_URL=${FBN_LUSID_API_URL} \
        -e FBN_APP_NAME=${FBN_APP_NAME} \
        -e FBN_PASSWORD=${FBN_PASSWORD} \
        -w /usr/src/ \
        -v $(pwd)/csharp/:/usr/src/ \
        mcr.microsoft.com/dotnet/sdk:6.0 dotnet test

test-java:
    docker run -t \
        -e FBN_LUSID_API_URL=${FBN_LUSID_API_URL} \
        -e FBN_TOKEN_URL=${FBN_TOKEN_URL} \
        -e FBN_ACCESS_TOKEN=${FBN_ACCESS_TOKEN} \
        -e FBN_USERNAME=${FBN_USERNAME} \
        -e FBN_CLIENT_ID=${FBN_CLIENT_ID} \
        -e FBN_CLIENT_SECRET=${FBN_CLIENT_SECRET} \
        -e FBN_LUSID_API_URL=${FBN_LUSID_API_URL} \
        -e FBN_APP_NAME=${FBN_APP_NAME} \
        -e FBN_PASSWORD=${FBN_PASSWORD} \
        -w /usr/src/ \
        -v $(pwd)/java/:/usr/src/ \
        maven:3.6.3-jdk-11 mvn -e -fae test