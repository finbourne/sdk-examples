FROM python:3.11.3

WORKDIR /usr/src/

RUN pip install poetry==1.6.*

COPY poetry.lock .

COPY pyproject.toml .

RUN poetry install

ENTRYPOINT poetry run pytest
