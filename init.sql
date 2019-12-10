--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.12
-- Dumped by pg_dump version 10.7 (Ubuntu 10.7-0ubuntu0.18.10.1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: category_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.category_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.category_seq OWNER TO mushroomer;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: category; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.category (
    id bigint DEFAULT nextval('public.category_seq'::regclass) NOT NULL,
    name text NOT NULL,
    people_id bigint
);


ALTER TABLE public.category OWNER TO mushroomer;

--
-- Name: logs_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.logs_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.logs_seq OWNER TO mushroomer;

--
-- Name: log; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.log (
    id bigint DEFAULT nextval('public.logs_seq'::regclass) NOT NULL,
    name text NOT NULL,
    task_id bigint NOT NULL,
    description text NOT NULL,
    "file_Path" text
);


ALTER TABLE public.log OWNER TO mushroomer;

--
-- Name: people_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.people_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.people_seq OWNER TO mushroomer;

--
-- Name: people; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.people (
    id bigint DEFAULT nextval('public.people_seq'::regclass) NOT NULL,
    "user" text NOT NULL,
    name text NOT NULL,
    surname text NOT NULL,
    email text NOT NULL,
    language text,
    admin boolean DEFAULT false,
    password text NOT NULL
);


ALTER TABLE public.people OWNER TO mushroomer;

--
-- Name: project_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.project_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.project_seq OWNER TO mushroomer;

--
-- Name: project; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.project (
    id bigint DEFAULT nextval('public.project_seq'::regclass) NOT NULL,
    name text NOT NULL,
    description text,
    public boolean,
    user_id bigint,
    repository text
);


ALTER TABLE public.project OWNER TO mushroomer;

--
-- Name: role; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.role (
    user_id bigint NOT NULL,
    status text NOT NULL
);


ALTER TABLE public.role OWNER TO mushroomer;

--
-- Name: slogs_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.slogs_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.slogs_seq OWNER TO mushroomer;

--
-- Name: slog; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.slog (
    id bigint DEFAULT nextval('public.slogs_seq'::regclass) NOT NULL,
    "time" timestamp with time zone NOT NULL,
    task_id bigint NOT NULL
);


ALTER TABLE public.slog OWNER TO mushroomer;

--
-- Name: spring_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.spring_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.spring_seq OWNER TO mushroomer;

--
-- Name: spring; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.spring (
    id bigint DEFAULT nextval('public.spring_seq'::regclass) NOT NULL,
    name text NOT NULL,
    description text,
    status text,
    wiki bigint,
    date timestamp with time zone
);


ALTER TABLE public.spring OWNER TO mushroomer;

--
-- Name: task_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.task_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.task_seq OWNER TO mushroomer;

--
-- Name: task; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.task (
    id bigint DEFAULT nextval('public.task_seq'::regclass) NOT NULL,
    theme text NOT NULL,
    tracker bigint NOT NULL,
    status text NOT NULL,
    priority text NOT NULL,
    description text,
    category bigint,
    spring bigint,
    date_over timestamp with time zone,
    "time" integer,
    percent text,
    file_path text,
    whocan bigint,
    project_id bigint
);


ALTER TABLE public.task OWNER TO mushroomer;

--
-- Name: tracker_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.tracker_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tracker_seq OWNER TO mushroomer;

--
-- Name: tracker; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.tracker (
    id bigint DEFAULT nextval('public.tracker_seq'::regclass) NOT NULL,
    name text NOT NULL,
    status text NOT NULL
);


ALTER TABLE public.tracker OWNER TO mushroomer;

--
-- Name: users_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.users_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_seq OWNER TO mushroomer;

--
-- Name: users; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.users (
    id bigint DEFAULT nextval('public.users_seq'::regclass) NOT NULL,
    email text NOT NULL,
    password text NOT NULL
);


ALTER TABLE public.users OWNER TO mushroomer;

--
-- Name: wiki_seq; Type: SEQUENCE; Schema: public; Owner: mushroomer
--

CREATE SEQUENCE public.wiki_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.wiki_seq OWNER TO mushroomer;

--
-- Name: wiki; Type: TABLE; Schema: public; Owner: mushroomer
--

CREATE TABLE public.wiki (
    id bigint DEFAULT nextval('public.wiki_seq'::regclass) NOT NULL,
    name text NOT NULL,
    description text,
    file_path text
);


ALTER TABLE public.wiki OWNER TO mushroomer;

--
-- Data for Name: category; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.category (id, name, people_id) FROM stdin;
1	Администрация	1
2	Рабочие	2
\.


--
-- Data for Name: log; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.log (id, name, task_id, description, "file_Path") FROM stdin;
1	Новая задача	1	Пользователь jaja.2010@yandex.ru добавил новое задание Клиентская часть	https://ru.wikipedia.org/wiki/REST
2	Новая задача	2	Пользователь jaja.2010@yandex.ru добавил новое задание Серверная часть	https://ru.wikipedia.org/wiki/REST
3	Завершение задачи	1	Пользователь jaja.2010@yandex.ru закрыл задачу Клиентская часть	https://ru.wikipedia.org/wiki/REST
4	Завершение задачи	2	Пользователь jaja.2010@yandex.ru закрыл задачу Серверная часть	https://ru.wikipedia.org/wiki/REST
5	Новая задача	3	Пользователь jaja.2010@yandex.ru добавил новое задание Отчёт	https://ru.wikipedia.org/wiki/REST
\.


--
-- Data for Name: people; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.people (id, "user", name, surname, email, language, admin, password) FROM stdin;
1	jaja.2010@yandex.ru	Андрей	Яковлев	jaja.2010@yandex.ru	ru	t	helloworld
2	farit@gmail.com	Фарит	Закиров	farit@gmail.com	en	f	hellworld
\.


--
-- Data for Name: project; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.project (id, name, description, public, user_id, repository) FROM stdin;
1	Курсовая	Система управления проектами	t	1	test
\.


--
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.role (user_id, status) FROM stdin;
1	Admin
2	Worker
\.


--
-- Data for Name: slog; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.slog (id, "time", task_id) FROM stdin;
1	2019-05-12 09:02:01.334+03	1
2	2019-05-12 09:02:30.935+03	2
3	2019-05-12 09:02:41.025+03	1
4	2019-05-12 09:02:51.213+03	2
5	2019-05-12 09:13:25.416+03	3
\.


--
-- Data for Name: spring; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.spring (id, name, description, status, wiki, date) FROM stdin;
1	Приложение	Клиент и сервер	Open	1	2019-05-12 09:01:26.414+03
\.


--
-- Data for Name: task; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.task (id, theme, tracker, status, priority, description, category, spring, date_over, "time", percent, file_path, whocan, project_id) FROM stdin;
1	Клиентская часть	1	Closed	Highest	Сделать клиентскую часть	1	1	2019-05-19 03:00:00+03	5	5	https://ru.wikipedia.org/wiki/REST	1	1
2	Серверная часть	2	Closed	Low	Сделать серверную часть	1	1	2019-05-19 03:00:00+03	5	5	https://ru.wikipedia.org/wiki/REST	1	1
3	Отчёт	3	Open	Medium	Сделать отчёт по курсовой	1	1	2019-05-19 03:00:00+03	5	5	https://ru.wikipedia.org/wiki/REST	1	1
\.


--
-- Data for Name: tracker; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.tracker (id, name, status) FROM stdin;
1	КЛИЕНТ	Open
2	СЕРВЕР	Open
3	ОТЧЁТ	Open
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.users (id, email, password) FROM stdin;
1	jaja.2010@yandex.ru	helloworld
2	farit@gmail.com	hellworld
\.


--
-- Data for Name: wiki; Type: TABLE DATA; Schema: public; Owner: mushroomer
--

COPY public.wiki (id, name, description, file_path) FROM stdin;
1	REST API	Изучить REST API	https://ru.wikipedia.org/wiki/REST
\.


--
-- Name: category_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.category_seq', 2, true);


--
-- Name: logs_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.logs_seq', 5, true);


--
-- Name: people_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.people_seq', 2, true);


--
-- Name: project_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.project_seq', 1, true);


--
-- Name: slogs_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.slogs_seq', 5, true);


--
-- Name: spring_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.spring_seq', 1, true);


--
-- Name: task_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.task_seq', 3, true);


--
-- Name: tracker_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.tracker_seq', 3, true);


--
-- Name: users_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.users_seq', 2, true);


--
-- Name: wiki_seq; Type: SEQUENCE SET; Schema: public; Owner: mushroomer
--

SELECT pg_catalog.setval('public.wiki_seq', 1, true);


--
-- Name: category category_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.category
    ADD CONSTRAINT category_pkey PRIMARY KEY (id);


--
-- Name: log logs_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.log
    ADD CONSTRAINT logs_pkey PRIMARY KEY (id);


--
-- Name: people people_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.people
    ADD CONSTRAINT people_pkey PRIMARY KEY (id);


--
-- Name: project project_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.project
    ADD CONSTRAINT project_pkey PRIMARY KEY (id);


--
-- Name: role roles_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT roles_pkey PRIMARY KEY (user_id);


--
-- Name: slog slogs_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.slog
    ADD CONSTRAINT slogs_pkey PRIMARY KEY (id);


--
-- Name: spring spring_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.spring
    ADD CONSTRAINT spring_pkey PRIMARY KEY (id);


--
-- Name: task task_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_pkey PRIMARY KEY (id);


--
-- Name: tracker tracker_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.tracker
    ADD CONSTRAINT tracker_pkey PRIMARY KEY (id);


--
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: wiki wiki_pkey; Type: CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.wiki
    ADD CONSTRAINT wiki_pkey PRIMARY KEY (id);


--
-- Name: task fk_category; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT fk_category FOREIGN KEY (category) REFERENCES public.category(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: category fk_people; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.category
    ADD CONSTRAINT fk_people FOREIGN KEY (people_id) REFERENCES public.people(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: task fk_project; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT fk_project FOREIGN KEY (project_id) REFERENCES public.project(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: task fk_spring; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT fk_spring FOREIGN KEY (spring) REFERENCES public.spring(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: slog fk_task; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.slog
    ADD CONSTRAINT fk_task FOREIGN KEY (task_id) REFERENCES public.task(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: log fk_task; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.log
    ADD CONSTRAINT fk_task FOREIGN KEY (task_id) REFERENCES public.task(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: task fk_tracker; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT fk_tracker FOREIGN KEY (tracker) REFERENCES public.tracker(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: project fk_user; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.project
    ADD CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES public.users(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: role fk_users; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT fk_users FOREIGN KEY (user_id) REFERENCES public.users(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: task fk_whocan; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT fk_whocan FOREIGN KEY (whocan) REFERENCES public.people(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: spring fk_wiki; Type: FK CONSTRAINT; Schema: public; Owner: mushroomer
--

ALTER TABLE ONLY public.spring
    ADD CONSTRAINT fk_wiki FOREIGN KEY (wiki) REFERENCES public.wiki(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

