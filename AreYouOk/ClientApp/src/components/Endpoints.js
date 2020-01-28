import React, { Component } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Label, Input, Container, Col, Row } from 'reactstrap';
import { faTrash } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import _ from 'lodash';

export class Endpoints extends Component {
  static displayName = Endpoints.name;

  constructor(props) {
    super(props);

    this.state = {
      endpoints: [],
      modalAdd: false,
      modalRemove: false,
      newEndpoint: null,
      rowToRemove: null
    };

    this.toggleAddModal = this.toggleAddModal.bind(this);
    this.toggleRemoveModal = this.toggleRemoveModal.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.addEndpoint = this.addEndpoint.bind(this);
    this.setRowToRemove = this.setRowToRemove.bind(this);
    this.removeEndpoint = this.removeEndpoint.bind(this);
  }

  componentWillMount() {
    this.fetchEndpoints().then(() => {
      console.log("fetched latest endpoints");
    });
  }

  async fetchEndpoints() {
    const response = await fetch('api/endpoints');
    const endpoints = await response.json();
    this.setState({ endpoints: endpoints });
  }

  toggleAddModal() {
    let modal = !this.state.modalAdd;
    this.setState({ modalAdd: modal });
  }

  toggleRemoveModal() {
    let modal = !this.state.modalRemove;
    this.setState({ modalRemove: modal });
  }

  async addEndpoint() {
    try {
      await fetch('api/endpoints', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          "endpoint": this.state.newEndpoint
        })
      });

      let endpoints = this.state.endpoints;
      endpoints[endpoints.length] = this.state.newEndpoint;
      this.setState({ endpoints: endpoints, newEndpoint: null });
      this.toggleAddModal();
    } catch (e) {
      console.error(e);
    }
  }

  async removeEndpoint() {
    try {
      await fetch('api/endpoints', {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          'endpoint': this.state.rowToRemove
        })
      })
      let endpoints = _.pull(this.state.endpoints,  this.state.rowToRemove);
      this.setState({endpoints: endpoints});
      this.toggleRemoveModal();
    } catch (e) {
      console.error(e);
    }

  }

  setRowToRemove(endpoint) {
    this.setState({ rowToRemove: endpoint });
    this.toggleRemoveModal();
  }

  handleChange(event) {
    this.setState({ newEndpoint: event.target.value })
  }

  render() {
    return (
      <div>
        <Container>
          <Row>
            <Col>
              <h5>Endpoints</h5>
            </Col>
            <Col>
              <Button color="primary" className="float-right" onClick={this.toggleAddModal}>Add endpoint</Button>
            </Col>
          </Row>
        </Container>
        <Container style={{ marginTop: 10 }}>
          <ul style={{ paddingInlineStart: 0 }}>
            {this.state.endpoints.map((endpoint, index) =>
              <li className="list-group-item d-flex justify-content-between align-items-center" key={index}>
                <Container>
                  <Row>
                    <Col>
                      {endpoint}
                    </Col>
                    <Col>
                      <Button color="danger" size="sm" className="float-right" onClick={() => this.setRowToRemove(endpoint)}><FontAwesomeIcon icon={faTrash}></FontAwesomeIcon></Button>
                    </Col>
                  </Row>
                </Container>
              </li>
            )}
          </ul>
        </Container>
        <Modal isOpen={this.state.modalAdd} toggle={this.toggleAddModal}>
          <ModalHeader toggle={this.toggleAddModal}>Add new endpoint</ModalHeader>
          <ModalBody>
            <Label for="endpointUrl">Endpoint</Label>
            <Input type="text" name="newEndpoint" id="endpointUrl" placeholder="URL" value={this.state.newEndpoint || ''} onChange={this.handleChange} />
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.addEndpoint}>Save</Button>{' '}
            <Button color="secondary" onClick={this.toggleAddModal}>Cancel</Button>
          </ModalFooter>
        </Modal>

        <Modal isOpen={this.state.modalRemove} toggle={this.toggleRemoveModal}>
          <ModalHeader toggle={this.toggleRemoveModal}>Add new endpoint</ModalHeader>
          <ModalBody>
            Are you sure you want to remove endpoint "{this.state.rowToRemove}"?
          </ModalBody>
          <ModalFooter>
            <Button color="danger" onClick={this.removeEndpoint}>Remove</Button>{' '}
            <Button color="secondary" onClick={this.toggleRemoveModal}>Cancel</Button>
          </ModalFooter>
        </Modal>
      </div>)
  }
}
