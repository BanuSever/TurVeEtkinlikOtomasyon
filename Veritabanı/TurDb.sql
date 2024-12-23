PGDMP  5                    |            Tur    16.2    16.2 .               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    58303    Tur    DATABASE     �   CREATE DATABASE "Tur" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
    DROP DATABASE "Tur";
                postgres    false            �            1255    58304    odeme_ekle()    FUNCTION     +  CREATE FUNCTION public.odeme_ekle() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- r_odeme değerini ödeme tutarı olarak al
    INSERT INTO odemeler (o_rezervasyonid, o_tutar, o_tarih)
    VALUES (NEW.r_id, NEW.r_odeme, NOW()); -- r_odeme değerini alıyoruz
    RETURN NEW;
END;
$$;
 #   DROP FUNCTION public.odeme_ekle();
       public          postgres    false            �            1255    58305    tur_kontrol_false_trigger()    FUNCTION     >  CREATE FUNCTION public.tur_kontrol_false_trigger() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Eğer t_kontrol değeri false olursa, rezervasyon tablosundaki r_kontrol'ü false yap
    IF NEW.t_kontrol = FALSE THEN
        UPDATE rezervasyon
        SET r_kontrol = FALSE
        WHERE r_turid = NEW.tur_id;
        
        -- Ödemeler tablosundaki o_kontrol'ü false yap
        UPDATE odemeler
        SET o_kontrol = FALSE
        WHERE o_rezervasyonid IN (SELECT r_id FROM rezervasyon WHERE r_turid = NEW.tur_id);
    END IF;

    RETURN NEW;
END;
$$;
 2   DROP FUNCTION public.tur_kontrol_false_trigger();
       public          postgres    false            �            1259    58306    giderler    TABLE     �   CREATE TABLE public.giderler (
    gider_id integer NOT NULL,
    otobus numeric DEFAULT 0,
    akaryakit numeric DEFAULT 0,
    hizmet numeric DEFAULT 0,
    calisan numeric DEFAULT 0,
    vergi numeric DEFAULT 0,
    ay integer,
    yil integer
);
    DROP TABLE public.giderler;
       public         heap    postgres    false            �            1259    58316    giderler_gider_id_seq    SEQUENCE     �   CREATE SEQUENCE public.giderler_gider_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public.giderler_gider_id_seq;
       public          postgres    false    215                       0    0    giderler_gider_id_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE public.giderler_gider_id_seq OWNED BY public.giderler.gider_id;
          public          postgres    false    216            �            1259    58317    kullanicilar    TABLE     �   CREATE TABLE public.kullanicilar (
    k_id integer NOT NULL,
    k_adi text NOT NULL,
    k_soyadi text NOT NULL,
    k_sifre text NOT NULL,
    k_eposta text NOT NULL,
    k_telefon text,
    k_rol text
);
     DROP TABLE public.kullanicilar;
       public         heap    postgres    false            �            1259    58322    kullanicilar_k_id_seq    SEQUENCE     �   CREATE SEQUENCE public.kullanicilar_k_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public.kullanicilar_k_id_seq;
       public          postgres    false    217                       0    0    kullanicilar_k_id_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE public.kullanicilar_k_id_seq OWNED BY public.kullanicilar.k_id;
          public          postgres    false    218            �            1259    58323    odemeler    TABLE     �   CREATE TABLE public.odemeler (
    o_id integer NOT NULL,
    o_rezervasyonid integer,
    o_tutar real NOT NULL,
    o_tarih date NOT NULL,
    o_kontrol boolean
);
    DROP TABLE public.odemeler;
       public         heap    postgres    false            �            1259    58326    odemeler_o_id_seq    SEQUENCE     �   CREATE SEQUENCE public.odemeler_o_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.odemeler_o_id_seq;
       public          postgres    false    219                        0    0    odemeler_o_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.odemeler_o_id_seq OWNED BY public.odemeler.o_id;
          public          postgres    false    220            �            1259    58327    rezervasyon    TABLE     �   CREATE TABLE public.rezervasyon (
    r_id integer NOT NULL,
    r_kisisayisi integer,
    r_turid integer,
    r_kulid integer,
    r_tarih text,
    r_odeme real,
    r_kontrol boolean
);
    DROP TABLE public.rezervasyon;
       public         heap    postgres    false            �            1259    58332    rezervasyon_r_id_seq    SEQUENCE     �   CREATE SEQUENCE public.rezervasyon_r_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.rezervasyon_r_id_seq;
       public          postgres    false    221            !           0    0    rezervasyon_r_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.rezervasyon_r_id_seq OWNED BY public.rezervasyon.r_id;
          public          postgres    false    222            �            1259    58333    turlar    TABLE       CREATE TABLE public.turlar (
    tur_id integer NOT NULL,
    tur_adi text NOT NULL,
    tur_aciklama text,
    tur_lokasyon text,
    tur_suresi text,
    tur_tarihi text,
    tur_ucreti real,
    foto_url text,
    tur_duraklar text,
    t_kontrol boolean
);
    DROP TABLE public.turlar;
       public         heap    postgres    false            �            1259    58338    turlar_tur_id_seq    SEQUENCE     �   CREATE SEQUENCE public.turlar_tur_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.turlar_tur_id_seq;
       public          postgres    false    223            "           0    0    turlar_tur_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.turlar_tur_id_seq OWNED BY public.turlar.tur_id;
          public          postgres    false    224            f           2604    58339    giderler gider_id    DEFAULT     v   ALTER TABLE ONLY public.giderler ALTER COLUMN gider_id SET DEFAULT nextval('public.giderler_gider_id_seq'::regclass);
 @   ALTER TABLE public.giderler ALTER COLUMN gider_id DROP DEFAULT;
       public          postgres    false    216    215            l           2604    58340    kullanicilar k_id    DEFAULT     v   ALTER TABLE ONLY public.kullanicilar ALTER COLUMN k_id SET DEFAULT nextval('public.kullanicilar_k_id_seq'::regclass);
 @   ALTER TABLE public.kullanicilar ALTER COLUMN k_id DROP DEFAULT;
       public          postgres    false    218    217            m           2604    58341    odemeler o_id    DEFAULT     n   ALTER TABLE ONLY public.odemeler ALTER COLUMN o_id SET DEFAULT nextval('public.odemeler_o_id_seq'::regclass);
 <   ALTER TABLE public.odemeler ALTER COLUMN o_id DROP DEFAULT;
       public          postgres    false    220    219            n           2604    58342    rezervasyon r_id    DEFAULT     t   ALTER TABLE ONLY public.rezervasyon ALTER COLUMN r_id SET DEFAULT nextval('public.rezervasyon_r_id_seq'::regclass);
 ?   ALTER TABLE public.rezervasyon ALTER COLUMN r_id DROP DEFAULT;
       public          postgres    false    222    221            o           2604    58343    turlar tur_id    DEFAULT     n   ALTER TABLE ONLY public.turlar ALTER COLUMN tur_id SET DEFAULT nextval('public.turlar_tur_id_seq'::regclass);
 <   ALTER TABLE public.turlar ALTER COLUMN tur_id DROP DEFAULT;
       public          postgres    false    224    223                      0    58306    giderler 
   TABLE DATA           `   COPY public.giderler (gider_id, otobus, akaryakit, hizmet, calisan, vergi, ay, yil) FROM stdin;
    public          postgres    false    215   8                 0    58317    kullanicilar 
   TABLE DATA           b   COPY public.kullanicilar (k_id, k_adi, k_soyadi, k_sifre, k_eposta, k_telefon, k_rol) FROM stdin;
    public          postgres    false    217   �8                 0    58323    odemeler 
   TABLE DATA           V   COPY public.odemeler (o_id, o_rezervasyonid, o_tutar, o_tarih, o_kontrol) FROM stdin;
    public          postgres    false    219   �9                 0    58327    rezervasyon 
   TABLE DATA           h   COPY public.rezervasyon (r_id, r_kisisayisi, r_turid, r_kulid, r_tarih, r_odeme, r_kontrol) FROM stdin;
    public          postgres    false    221   
:                 0    58333    turlar 
   TABLE DATA           �   COPY public.turlar (tur_id, tur_adi, tur_aciklama, tur_lokasyon, tur_suresi, tur_tarihi, tur_ucreti, foto_url, tur_duraklar, t_kontrol) FROM stdin;
    public          postgres    false    223   �:       #           0    0    giderler_gider_id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public.giderler_gider_id_seq', 37, true);
          public          postgres    false    216            $           0    0    kullanicilar_k_id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public.kullanicilar_k_id_seq', 16, true);
          public          postgres    false    218            %           0    0    odemeler_o_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.odemeler_o_id_seq', 22, true);
          public          postgres    false    220            &           0    0    rezervasyon_r_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.rezervasyon_r_id_seq', 26, true);
          public          postgres    false    222            '           0    0    turlar_tur_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.turlar_tur_id_seq', 34, true);
          public          postgres    false    224            q           2606    58345    giderler giderler_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.giderler
    ADD CONSTRAINT giderler_pkey PRIMARY KEY (gider_id);
 @   ALTER TABLE ONLY public.giderler DROP CONSTRAINT giderler_pkey;
       public            postgres    false    215            s           2606    58347    kullanicilar kullanicilar_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.kullanicilar
    ADD CONSTRAINT kullanicilar_pkey PRIMARY KEY (k_id);
 H   ALTER TABLE ONLY public.kullanicilar DROP CONSTRAINT kullanicilar_pkey;
       public            postgres    false    217            u           2606    58349    odemeler odemeler_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.odemeler
    ADD CONSTRAINT odemeler_pkey PRIMARY KEY (o_id);
 @   ALTER TABLE ONLY public.odemeler DROP CONSTRAINT odemeler_pkey;
       public            postgres    false    219            w           2606    58351    rezervasyon rezervasyon_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.rezervasyon
    ADD CONSTRAINT rezervasyon_pkey PRIMARY KEY (r_id);
 F   ALTER TABLE ONLY public.rezervasyon DROP CONSTRAINT rezervasyon_pkey;
       public            postgres    false    221            y           2606    58353    turlar turlar_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.turlar
    ADD CONSTRAINT turlar_pkey PRIMARY KEY (tur_id);
 <   ALTER TABLE ONLY public.turlar DROP CONSTRAINT turlar_pkey;
       public            postgres    false    223            }           2620    58354 #   rezervasyon rezervasyon_tetikleyici    TRIGGER     }   CREATE TRIGGER rezervasyon_tetikleyici AFTER INSERT ON public.rezervasyon FOR EACH ROW EXECUTE FUNCTION public.odeme_ekle();
 <   DROP TRIGGER rezervasyon_tetikleyici ON public.rezervasyon;
       public          postgres    false    221    225            ~           2620    58355    turlar tur_kontrol_false    TRIGGER     �   CREATE TRIGGER tur_kontrol_false AFTER UPDATE ON public.turlar FOR EACH ROW WHEN ((old.t_kontrol IS DISTINCT FROM new.t_kontrol)) EXECUTE FUNCTION public.tur_kontrol_false_trigger();
 1   DROP TRIGGER tur_kontrol_false ON public.turlar;
       public          postgres    false    223    223    226            z           2606    58356 &   odemeler odemeler_o_rezervasyonid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.odemeler
    ADD CONSTRAINT odemeler_o_rezervasyonid_fkey FOREIGN KEY (o_rezervasyonid) REFERENCES public.rezervasyon(r_id) ON DELETE CASCADE;
 P   ALTER TABLE ONLY public.odemeler DROP CONSTRAINT odemeler_o_rezervasyonid_fkey;
       public          postgres    false    221    4727    219            {           2606    58361 $   rezervasyon rezervasyon_r_kulid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.rezervasyon
    ADD CONSTRAINT rezervasyon_r_kulid_fkey FOREIGN KEY (r_kulid) REFERENCES public.kullanicilar(k_id);
 N   ALTER TABLE ONLY public.rezervasyon DROP CONSTRAINT rezervasyon_r_kulid_fkey;
       public          postgres    false    4723    217    221            |           2606    58366 $   rezervasyon rezervasyon_r_turid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.rezervasyon
    ADD CONSTRAINT rezervasyon_r_turid_fkey FOREIGN KEY (r_turid) REFERENCES public.turlar(tur_id) ON DELETE CASCADE;
 N   ALTER TABLE ONLY public.rezervasyon DROP CONSTRAINT rezervasyon_r_turid_fkey;
       public          postgres    false    223    4729    221               �   x�]�Q� �o8̆�P�.{�sl��͠(&OgP-�?�h�^�H�:�%���D#iy�C����=0�d���ӝ�
�S�����	=M9�q��w��+t^��Z9� �򶞅^��(�����4�*�U�U&��,7y`W�����qA����k�u������u?��o���b�QzE#��~?��f�         �   x�3�LL��̃��F�&�fP��������bC� �1grj>CՂ�(*�@B\�����ũI�1yp&L����.�d;9��r���q&%� ��s3s���s95L̀zMu-�͍9K+��8�lp
r�8�������#||�l�ٚ�T��됑_�d������511����� �M          K   x�e���0��.E��X�	�f��^��d��'���Q�nc�2�<�C}��2��w�U��fS'e�ffܬ�         h   x�m���0D��
`�8��*�L�%�W���<��\1S3��>�B��8��� XN!�#W�܉ʈ|�I�Ͳ!����̉d�J�'j�
�o���}��         �  x��Y�n9]W���� G��8������8��3H�@�RQ2S,�PU���40����6�x���/�s/Y/�z3��f����{�!=����+)�d����*��������+
��R�Q��Z,T����/�K�r-VJ�/��~sWc��֢֕�T!T��4+�qZeۛ����Z��M��XLӪ��6w���ޯE%����(����������x�l�<8�F�N�$�8��\���aT�`���O���{�Eڊ�͝)���"M ���a�g�~����ޮ�K|����O"��2��g$�@��3�]oo �UYe�TS�d��3�2��$��*�TޢJxd\��a������h<o�Ix#��L��"��!rE�����']+����H�+B�0�c��H#��V�VL�Xdi# �*��*d��ݮ3e)���b��ID�vMo6�Jύ���B�c<�h��9C�rh�yi�YV��O�H���]F:w�Nm!���Ʀ����$�JS:!���|�l��/�X\�L�,�Kz�$$>�l���s����f�^m�����6�λ	�����)��88/��2�ݠ��U���9m)"�h���>y����N�B%ߢ��K�I�C�V�G;ɞtt�˜bќ���C����>��w�s�ge�Kq�'�B4\�<F��=�~O�2�E��r�v�h�6�6QL�!���F]�UD�ͭU��bȬ�{*�%�I������Ŕ�|�L�)8�]JLF�a�Ҕ&0D���ݧ�.�7��~����*��:��r	�b��F�d>~�C�Y؅J�鼓e��T�8��9�eR9=NJY�2���/�5N�4�=P˓cG^R��%���?x(�ҺPK�،��TqMZ���1�s
4��q_*;A#�8ؒ���[0�xl�mobB����D�$h����
DO',ǌ���a�`iн6��w������IJ��c��"�����(�|����)�����_�k]���e%���6�-�T+�u�+�������3�2�Kʺ�:�LLK�.�]�&/:5�5��>Ҥy�G/��M9��1ZσS�)~Vu�|\�W,X��M咶�+�6�	�K)*�?%	�ʮ$`���4�s��.��At���(��j�2b�X�Z�S�.�����4�R��K>R�:O��0U�DKJ1ML��*�8�*긠i~d)Ei �����(2z��^#�g����Q�Gt.�u-A�����Jp����P��D�.K�V�o�E��R_��t�s�BXƕ/X~7Įd+NKSZIl�ʊ!OU���ȝ�����z�|S�ʨLL�"΋L%"W+��nJ�?I�#1����pxV�W���^8p���8�8��Z(TK��ɢ�ǆIXU/&��m��x�v �aE���~ u-�~*l���Y�Zg`�c�Z�b�����F#Wxy��(cQ�6�!�������{���	iV2ۜЦfVr��H�Э
��y�m*�`4>�8<��R�f/P*j��Ⴥ�b+��X���R��3i�/�Wj��[� �H�r9�׈���:�f-6�1�_#"�PӰ�M�F)�m%��D�Gz"��j'Nv� u'ęg���t|P+�d�s�^4�h�*�9� oaeT��s��$�M�����
N�5-�zB�>�����MA����%C�C�8>�"�ܵ(�(�k�^�0	�
����v� �1�ʌ#"F�X
ʢ*�!'jݰ��(��ٸ���ˬ`�A+W,F��AA��(B^��!�2>�?�3�#$�W�~<�������j�V�~������i�j�,��Jw�����u�"Q�/�_���Ӛ��OX����%��n�Mg�;B�h���SsH�������D4��<-h66�4qy� ϛm���_ÅE$��R�g���5�7߷=
�}�:���	�J�CGO֋�"-3F�E�K�G�R���D��yG��x�8N��p��c�8s���#/�s"�y�W.\�	�)y���1��̞��Ñ�$�Ђ�/ J���S���4����=��'lti������D}Jg�"t�H��
 G$B���o)�siq�tY�N�4�N@���k�.َ�y��Ơĺ�-���I���9�D�3|�Mw�6���2o/�������mّ�)����ˇ7��.�kl3���ؤ��B��-�����)�f
3�w=�sHÓ��A���-L(�moP��Gv�R�WL�wҲr���2)�%���_�����8��(���<�����eJ<�����/yI��j�E�k:%�{o���M]6��v���c���<�v3%�H���<�v�@���N3�Z��"]P�OY5��@�}iu���à�`����Š-GL>�A�TӅ!�<��W�4T�<�C~juKS`-��Ԥ_�v��B[Ȱ{,������s��+�N��	I7������� p����WA*�[��ͨ�A [������Tڊj R���D�#>��Z�:*ɲ�+��â�O��џ���7e6�/�39�Y�mR�3���F���S�;	�t���;ѱ�o���I�o�g8�N�g�UZ���X�3n�7:>�؝�s�^S�lu���q�	]���P�v�9�P�6��P��a�9�5�:�Q�%�=�;��F�����J�ͩ��A�W�2&~C�3���y^?�ڌkߍR5�e6h. �y���Ұ�x�!\��
R����ޝ��^g����Z(���\=�o��-��Qp	c��/����F_H��N�
C��#r���I��C���壸�����J�{����i��u����q� _��CV����}��}ZQ�Żo ?���L8^8q��aO���&��jܠ���fts[+��.u�#{�Oh8ʲ��sp���&i|u��88v��i��ݰ�'[h����@9G����'O��$O     