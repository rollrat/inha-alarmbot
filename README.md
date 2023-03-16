# 인하대 알림봇 (Inha Univ Alarm/Notice Bot)

인하대 알림봇은 인하대 공식 홈페이지 및 학과 홈페이지의 공지사항을 일정 시간마다
크롤링하여 구독자에게 알림을 보내주는 서비스입니다.

## 실행

```sh
wget http://security.ubuntu.com/ubuntu/pool/main/o/openssl1.0/libssl1.0.0_1.0.2n-1ubuntu5_amd64.deb
sudo dpkg -i libssl1.0.0_1.0.2n-1ubuntu5_amd64.deb
```

## 운영

디스코드 알림봇: http://inhaalarmbot.kro.kr

텔레그램 알림봇: https://t.me/inhaalarmbot

## 구현 현황

- [x] 인하대 공식 홈페이지 공지사항 알림
- [x] 모든 학과 홈페이지 공지사항 알림
- [ ] 카카오톡 알림봇 (무기한 연기)
- [X] 디스코드 알림봇
- [x] 텔레그램 알림봇

## 성능 및 개선사항 문의

`rollrat.cse@gmail.com`

## 라이센스

```
MIT License

Copyright (c) 2020 rollrat

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
