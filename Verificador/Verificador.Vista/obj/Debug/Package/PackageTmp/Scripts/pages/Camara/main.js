/*
 *  Copyright (c) 2015 The WebRTC project authors. All Rights Reserved.
 *
 *  Use of this source code is governed by a BSD-style license
 *  that can be found in the LICENSE file in the root of the source
 *  tree.
 */

'use strict';

//https://github.com/webrtc/samples/tree/gh-pages/src/content/getusermedia/canvas
// Put variables in global scope to make them available to the browser console.
var video = window.video =document.querySelector('video');
var canvas = window.canvas = document.querySelector('canvas');
canvas.width = 320;
canvas.height = 240;

var constraints = {
    audio: false,
    video: true,
    mirror: true
};

function handleSuccess(stream) {
    window.stream = stream; // make stream available to browser console
    video.srcObject = stream;
}

function handleError(error) {
    console.log('navigator.getUserMedia error: ', error);
}

navigator.mediaDevices.getUserMedia(constraints).
    then(handleSuccess).catch(handleError);