const express = require('express');
const AccessController = require('../controllers/accessController');

const router = express.Router();
const accessController = new AccessController();

router.post('/request-access', accessController.createAccessRequest.bind(accessController));
router.post('/revoke-access', accessController.revokeAccessRequest.bind(accessController));
router.post('/change-permission', accessController.changeAccessPermission.bind(accessController));

module.exports = router;